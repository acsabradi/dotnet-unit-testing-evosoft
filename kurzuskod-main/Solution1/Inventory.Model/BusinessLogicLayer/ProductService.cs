using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Claims;

namespace Inventory.Model
{
    internal class ProductService : IProductService
    {
        private readonly INavContextFactory navContextFactory;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IApplicationServices applicationServices;
        private readonly IFileInvoiceWriter fileInvoiceWriter;

        public ProductService(IProductRepository productRepository,
                              ICategoryRepository categoryRepository,
                              INavContextFactory navContextFactory,
                              IApplicationServices applicationServices,
                              IFileInvoiceWriter fileInvoiceWriter)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            this.navContextFactory = navContextFactory ?? throw new ArgumentNullException(nameof(navContextFactory));
            this.applicationServices = applicationServices ?? throw new ArgumentNullException(nameof(applicationServices));
            this.fileInvoiceWriter = fileInvoiceWriter ?? throw new ArgumentNullException(nameof(fileInvoiceWriter));
        }

        private IReadOnlyList<Product> ReadProducts()
        {
            var products = productRepository.ReadProducts().Where(p => !p.Discontinued).ToList();
            applicationServices.Logger.LogInformation("Products read");
            return products;
        }

        private IReadOnlyList<Category> ReadCategories()
        {
            var categories = categoryRepository.ReadCategories();
            foreach (var c in categories)
            {
                c.Products = new List<Product>();
            }
            applicationServices.Logger.LogInformation("Categories read");
            return categories;
        }



        public IReadOnlyList<Category> GetCategoriesMaster()
        {
            var products = ReadProducts();
            var categories = ReadCategories();
            foreach (var product in products)
            {
                product.Category = categories.Single(c => c.CategoryID == product.CategoryID);
                product.Category.Products.Add(product);
            }
            return categories;
        }

        public void CreateInvoce(string productName, int requiredCount, string fileName)
        {
            // TODO: Add stock handling
            var products = ReadProducts();
            var product = products.Single(p => p.Name == productName);
            var price = product.UnitPrice * (applicationServices.DateProvider.Now.DayOfWeek == DayOfWeek.Friday ? 0.95M : 1M) * requiredCount * 1.27M;
            var invoiceLine = $"{productName}\t{requiredCount}\t{product.QuantityPerUnit}\t{price}\t{applicationServices.DateProvider.Now.ToShortDateString()}";
            fileInvoiceWriter.WriteInvoiceLine(fileName, invoiceLine);
            SendFileToNav(fileName);
            applicationServices.Logger.LogInformation("Invoice sent");
        }

        private void SendFileToNav(string fileName)
        {
            IIdentity identity = applicationServices.UserProvider.User;
            using (INavContext navContext = navContextFactory.CreateNavContext())
            {
                navContext.SendFile(fileName, "Nav:" + identity.Name);
            }
        }
    }
}
