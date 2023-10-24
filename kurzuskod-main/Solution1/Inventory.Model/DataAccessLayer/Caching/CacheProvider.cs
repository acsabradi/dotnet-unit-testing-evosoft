using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model.DataAccessLayer
{
    internal class CacheProvider : ICacheProvider
    {
        public IReadOnlyList<Product> ProductCache { get; set; }
        public IReadOnlyList<Category> CategoryCache { get; set; }
    }
}
