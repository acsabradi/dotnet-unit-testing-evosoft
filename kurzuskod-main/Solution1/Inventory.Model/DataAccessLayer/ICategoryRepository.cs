using System.Collections.Generic;

namespace Inventory.Model
{
    public interface ICategoryRepository
    {
        IReadOnlyList<Category> ReadCategories();
    }
}
