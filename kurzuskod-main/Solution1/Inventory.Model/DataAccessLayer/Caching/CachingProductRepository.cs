using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model.DataAccessLayer
{
    internal class CachingProductRepository : IProductRepository
    {
        private readonly IProductRepository productRepository;
        private readonly ICacheProvider cacheProvider;

        public CachingProductRepository(IProductRepository productRepository, ICacheProvider cacheProvider)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.cacheProvider = cacheProvider ?? throw new ArgumentNullException(nameof(cacheProvider));
        }

        public IReadOnlyList<Product> ReadProducts()
        {
            if (cacheProvider.ProductCache==null)
            {
                cacheProvider.ProductCache = productRepository.ReadProducts();
            }
            return cacheProvider.ProductCache;
        }
    }
}
