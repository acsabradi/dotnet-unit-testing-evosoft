using Inventory.Model;
using Inventory.ViewModel;
using Moq;

namespace Inventory.VMTest
{
    public class Tests
    {
        [Test]
        public void TestVm()
        {
            // Arrange
            Mock<IProductService> productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(p => p.GetCategoriesMaster()).Returns(new List<Category>());

            Mock<IDialogFactory> dialogFactoryMock = new Mock<IDialogFactory> { DefaultValue=DefaultValue.Mock };
            Mock.Get(dialogFactoryMock.Object.CreateDialog()).Setup(d=>d.ShowDialog()).Returns(true);

            MainViewModel mainViewModel = new MainViewModel(productServiceMock.Object, dialogFactoryMock.Object);
            mainViewModel.RequiredCount = 13;
            mainViewModel.SelectedProduct = new ProductViewModel { ProductName = "P1" };

            // Act
            mainViewModel.CreateInvoice();

            // Assert
            Assert.That(mainViewModel.RequiredCount, Is.EqualTo(0));

            productServiceMock.Verify(p => p.CreateInvoce("P1", 13, It.IsAny<string>()));
        }
    }
}