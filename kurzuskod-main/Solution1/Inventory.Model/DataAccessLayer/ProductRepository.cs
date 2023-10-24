using System.Collections.Generic;

namespace Inventory.Model
{
    internal class ProductRepository : XmlFileRepository, IProductRepository
    {

        public IReadOnlyList<Product> ReadProducts()
        {
            return Read<Product>("products.xml");
        }
    }
}
