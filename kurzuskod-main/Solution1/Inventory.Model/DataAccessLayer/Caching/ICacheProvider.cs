using System.Collections.Generic;

namespace Inventory.Model.DataAccessLayer
{
    public interface ICacheProvider
    {
        IReadOnlyList<Category> CategoryCache { get; set; }
        IReadOnlyList<Product> ProductCache { get; set; }
    }
}