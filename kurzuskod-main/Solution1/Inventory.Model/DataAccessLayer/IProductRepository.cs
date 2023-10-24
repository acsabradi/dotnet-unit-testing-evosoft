using System.Collections.Generic;

namespace Inventory.Model
{
    public interface IProductRepository // CRUD
    {
        IReadOnlyList<Product> ReadProducts();
    }
}
