using Inventory.Model;
using Moq;
using System.ComponentModel;
using System.Security.Principal;

namespace Inventory.Test
{
    public class Tests
    {
        private ILogger CreateLoggerMock() => new Mock<ILogger>().Object;
        private INavContext CreateNavContextMock()
        {
            Mock<INavContext> navContextMock = new Mock<INavContext>(MockBehavior.Strict);
            navContextMock.Setup(n => n.SendFile(It.IsAny<string>(), It.IsAny<string>()));
            navContextMock.Setup(n => n.Dispose());
            return navContextMock.Object;
        }

        private INavContextFactory CreateNavContextFactoryMock()
        {
            Mock<INavContextFactory> navContextFactoryMock = new Mock<INavContextFactory>(MockBehavior.Strict);
            INavContext navContextMock = CreateNavContextMock();
            navContextFactoryMock.Setup(n => n.CreateNavContext()).Returns(navContextMock);
            return navContextFactoryMock.Object;
        }

        private IProductRepository CreateProductRepositoryMock(List<Product> products)
        {
            var productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(x => x.ReadProducts())
                                 .Returns(products.ToList());
            return productRepositoryMock.Object;
        }

        private ICategoryRepository CreateCategoryRepositoryMock(List<Category> categories)
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(x => x.ReadCategories())
                                  .Returns(categories.ToList());
            return categoryRepositoryMock.Object;
        }

        private IDateProvider CreateDateProviderMock(DateTime dateTime)
        {
            var dateProviderMock = new Mock<IDateProvider>(MockBehavior.Strict);
            dateProviderMock.SetupGet(d => d.Now).Returns(dateTime);
            return dateProviderMock.Object;
        }
        private IDateProvider CreateDateProviderMock() => CreateDateProviderMock(DateTime.Now);
        private IUserProvider CreateUserProviderMock(string userName)
        {
            Mock<IUserProvider> userProviderMock = new Mock<IUserProvider> { DefaultValue = DefaultValue.Mock };
            Mock.Get(userProviderMock.Object.User).SetupGet(i => i.Name).Returns("Akos");

            //Mock<IUserProvider> userProviderMock = new Mock<IUserProvider>();
            //userProviderMock.SetupGet(u => u.User.Name).Returns("Akos");

            return userProviderMock.Object;
        }
        private IUserProvider CreateUserProviderMock() => CreateUserProviderMock("Akos");

        private IFileInvoiceWriter CreateFileInvoiceWriterMock()
        {
            return Mock.Of<IFileInvoiceWriter>();
        }


        private IApplicationServices CreateApplicationServicesMock(DateTime? date = default, string userName = default)
        {
            Mock<IApplicationServices> appServicesMock = new Mock<IApplicationServices>();
            ILogger logger = CreateLoggerMock();
            IDateProvider dateProvider = date.HasValue ? CreateDateProviderMock(date.Value) : CreateDateProviderMock();
            IUserProvider userProvider = userName != null ? CreateUserProviderMock(userName) : CreateUserProviderMock();
            appServicesMock.SetupGet(a => a.UserProvider).Returns(userProvider);
            appServicesMock.SetupGet(a => a.DateProvider).Returns(dateProvider);
            appServicesMock.SetupGet(a => a.Logger).Returns(logger);

            return appServicesMock.Object;
        }


        [Test]
        public void ReadBaseTest()
        {
            var categories = new List<Category> { new Category { CategoryID = 1 } };
            var products = new List<Product>
            {
                new Product { CategoryID = categories[0].CategoryID, Discontinued=false },
                new Product { CategoryID =  categories[0].CategoryID, Discontinued=false },
                new Product { CategoryID =  categories[0].CategoryID, Discontinued=false },
            };
            var productService = new ProductService(CreateProductRepositoryMock(products),
                                                    CreateCategoryRepositoryMock(categories),
                                                    CreateNavContextFactoryMock(),
                                                    CreateApplicationServicesMock(),
                                                    CreateFileInvoiceWriterMock()
                                                );



            IReadOnlyList<Category> actualResult = productService.GetCategoriesMaster();

            Assert.That(actualResult, Has.Count.EqualTo(categories.Count));
            Assert.That(actualResult[0].Products, Has.Count.EqualTo(products.Count));
        }


        [Test]
        public void TestDiscontinuedProducts()
        {
            // Arrange
            var categories = new List<Category> { new Category { CategoryID = 1 } };
            var currentProduct = new Product { CategoryID = categories[0].CategoryID, Discontinued = false, Name = "C" };
            var products = new List<Product>
            {
                currentProduct,
                new Product { CategoryID =  categories[0].CategoryID, Discontinued=true },
                new Product { CategoryID =  categories[0].CategoryID, Discontinued=true },
            };
            var productService = new ProductService(
                CreateProductRepositoryMock(products),
                CreateCategoryRepositoryMock(categories),
                CreateNavContextFactoryMock(),
                CreateApplicationServicesMock(),
                CreateFileInvoiceWriterMock()
            );

            // Act
            IReadOnlyList<Category> actualResult = productService.GetCategoriesMaster();

            // Assert
            Assert.That(actualResult[0].Products, Has.All.Property(nameof(Product.Discontinued)).False);
            Assert.That(actualResult[0].Products, Has.Count.EqualTo(1));
            Assert.That(actualResult[0].Products[0], Has.Property(nameof(Product.Name)).EqualTo(currentProduct.Name));
        }

        //[Test]
        //public void TestFridaySale()
        //{

        //    var categories = new List<Category> { new Category { CategoryID = 1 } };

        //    var actualPrice = 85;
        //    var discountRate = 0.95M;
        //    DateTime fridayDate = new DateTime(2023, 10, 20);

        //    var products = new List<Product>
        //    {
        //        new Product { CategoryID = categories[0].CategoryID, Discontinued = false, UnitPrice=actualPrice }
        //    };


        //    var productService = new ProductService(
        //        CreateProductRepositoryMock(products),
        //        CreateCategoryRepositoryMock(categories),
        //        CreateNavContextFactoryMock(),
        //        CreateApplicationServicesMock(date: fridayDate),
        //        CreateFileInvoiceWriterMock()
        //    );

        //    var actualResult = productService.GetCategoriesMaster();
        //    var actualResultPrice = actualResult[0].Products[0].UnitPrice;
        //    var expectedPrice = actualPrice * discountRate;

        //    Assert.That(actualResultPrice, Is.EqualTo(expectedPrice).Within(0.00001M));

        //}

        //private static IEnumerable<DateTime> GeneratePurchaseDates()
        //{
        //    for (int day = 14; day <= 19; day++)
        //    {
        //        yield return new DateTime(2023, 10, day);
        //    }
        //}

        //[TestCaseSource(nameof(GeneratePurchaseDates))]
        //public void TestRegularPrice(DateTime purchaseDay)
        //{
        //    var categories = new List<Category> { new Category { CategoryID = 1 } };

        //    var actualPrice = 85;

        //    var products = new List<Product>
        //    {
        //        new Product { CategoryID = categories[0].CategoryID, Discontinued = false, UnitPrice=actualPrice }
        //    };


        //    var productService = new ProductService(
        //        CreateProductRepositoryMock(products),
        //        CreateCategoryRepositoryMock(categories),
        //        CreateNavContextFactoryMock(),
        //        CreateApplicationServicesMock(date: purchaseDay),
        //        CreateFileInvoiceWriterMock()
        //    );

        //    var actualResult = productService.GetCategoriesMaster();
        //    var actualResultPrice = actualResult[0].Products[0].UnitPrice;

        //    Assert.That(actualResultPrice, Is.EqualTo(actualPrice).Within(0.00001M));
        //}

        // TODO: Product-Categories matching


        [Test]
        public void TestNavSendingOccurs()
        {
            var category = new Category { CategoryID = 1 };
            var categories = new List<Category> { category };

            var productName = "P1";
            var products = new List<Product> { new Product { CategoryID = category.CategoryID, Discontinued = false, Name = productName } };

            var navContextMock = new Mock<INavContext>(MockBehavior.Strict);
            navContextMock.Setup(n => n.SendFile(It.IsAny<string>(), It.IsAny<string>()));
            navContextMock.Setup(n => n.Dispose());
            Mock<INavContextFactory> navContextFactoryMock = new Mock<INavContextFactory>();
            navContextFactoryMock.Setup(n => n.CreateNavContext()).Returns(navContextMock.Object);


            var productService = new ProductService(
                CreateProductRepositoryMock(products),
                CreateCategoryRepositoryMock(categories),
                navContextFactoryMock.Object,
                CreateApplicationServicesMock(),
                CreateFileInvoiceWriterMock()
            );


            productService.CreateInvoce(productName, 0, "invoice.txt");

            navContextMock.Verify(n => n.SendFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

        }


        [Test]
        public void TestNavSendingFileName()
        {
            var category = new Category { CategoryID = 1 };
            var categories = new List<Category> { category };

            var productName = "P1";
            var products = new List<Product> { new Product { CategoryID = category.CategoryID, Discontinued = false, Name = productName } };
            var fileName = "invoice.txt";

            Mock<INavContextFactory> navContextFactoryMock = new Mock<INavContextFactory> { DefaultValue = DefaultValue.Mock };
            INavContext navContextMock = navContextFactoryMock.Object.CreateNavContext();
            Mock.Get(navContextMock).Setup(n => n.SendFile(fileName, It.IsAny<string>()));


            var productService = new ProductService(
                CreateProductRepositoryMock(products),
                CreateCategoryRepositoryMock(categories),
                navContextFactoryMock.Object,
                CreateApplicationServicesMock(),
                CreateFileInvoiceWriterMock()
            );


            productService.CreateInvoce(productName, 0, fileName);

            Mock.Get(navContextMock).Verify(n => n.SendFile(fileName, It.IsAny<string>()), Times.Once());
            //navContextMock.VerifyNoOtherCalls();
        }

        [Test]
        public void TestNavSendingUserName()
        {
            var category = new Category { CategoryID = 1 };
            var categories = new List<Category> { category };

            var productName = "P1";
            var products = new List<Product> { new Product { CategoryID = category.CategoryID, Discontinued = false, Name = productName } };

            string userName = "Akos";
            string expectedNavUserName = "Nav:" + userName;

            Mock<INavContextFactory> navContextFactoryMock = new Mock<INavContextFactory> { DefaultValue = DefaultValue.Mock };
            INavContext navContextMock = navContextFactoryMock.Object.CreateNavContext();

            Mock.Get(navContextMock).Setup(n => n.SendFile(It.IsAny<string>(), expectedNavUserName));

            IApplicationServices applicationServices = CreateApplicationServicesMock(userName: userName);
            var productService = new ProductService(
                CreateProductRepositoryMock(products),
                CreateCategoryRepositoryMock(categories),
                navContextFactoryMock.Object,
                applicationServices,
                CreateFileInvoiceWriterMock()
            );


            productService.CreateInvoce(productName, 0, "invoice.txt");

            Mock.Get(applicationServices.UserProvider.User).Verify(u => u.Name);
            Mock.Get(navContextMock).Verify(n => n.SendFile(It.IsAny<string>(), expectedNavUserName), Times.Once());

        }


        [Test]
        public void TestInvoicePricingOnSale()
        {
            var category = new Category { CategoryID = 1 };
            var categories = new List<Category> { category };

            var productName = "P1";
            var productPrice = 85M;
            var products = new List<Product> { new Product { CategoryID = category.CategoryID, Discontinued = false, Name = productName, UnitPrice = productPrice } };

            var requiredCount = 3;

            decimal saleReduction = 0.95M;
            decimal delta = 0.000001M;
            decimal vat = 0.27M;
            decimal expectedPrice = requiredCount * productPrice * (1 + vat) * saleReduction;

            Mock<IFileInvoiceWriter> fileInvoiceWriterMock = new Mock<IFileInvoiceWriter>(MockBehavior.Strict);
            string invoiceLine = string.Empty;
            fileInvoiceWriterMock.Setup(f => f.WriteInvoiceLine(It.IsAny<string>(), It.IsAny<string>()))
                                 .Callback<string, string>((fName, line) => invoiceLine = line);

            ProductService productService = new ProductService(CreateProductRepositoryMock(products),
                                                               CreateCategoryRepositoryMock(categories),
                                                               CreateNavContextFactoryMock(),
                                                               CreateApplicationServicesMock(date: new DateTime(2023, 10, 20)),
                                                               fileInvoiceWriterMock.Object
                                                             );

            productService.CreateInvoce(productName, requiredCount, "invoice.txt");
            fileInvoiceWriterMock.Verify(f => f.WriteInvoiceLine(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

            decimal d = decimal.Parse(invoiceLine.Split('\t')[3]);
            Assert.That(d, Is.EqualTo(expectedPrice).Within(0.00001M));
        }

        // TODO: Not on sale

        [Test]
        public void TestInvoiceFormat()
        {

        }

    }
}