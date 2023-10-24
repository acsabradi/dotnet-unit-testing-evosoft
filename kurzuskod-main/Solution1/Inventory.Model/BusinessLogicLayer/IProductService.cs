using System.Collections.Generic;

namespace Inventory.Model
{
    public interface IProductService
    {
        void CreateInvoce(string productName, int requiredCount, string fileName);
        IReadOnlyList<Category> GetCategoriesMaster();
    }
}