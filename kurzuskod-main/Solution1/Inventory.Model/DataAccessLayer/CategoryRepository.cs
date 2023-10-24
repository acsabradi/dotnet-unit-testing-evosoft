using System.Collections.Generic;

namespace Inventory.Model
{
    internal class CategoryRepository : XmlFileRepository, ICategoryRepository
    {
        public IReadOnlyList<Category> ReadCategories()
        {
            return Read<Category>("categories.xml");
        }
    }
}
