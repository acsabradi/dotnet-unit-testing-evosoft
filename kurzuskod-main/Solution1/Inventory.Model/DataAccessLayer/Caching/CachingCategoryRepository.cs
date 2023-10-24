using System;
using System.Collections.Generic;

namespace Inventory.Model.DataAccessLayer
{
    // proxy
    internal class CachingCategoryRepository : ICategoryRepository
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ICacheProvider cacheProvider;

        public CachingCategoryRepository(ICategoryRepository categoryRepository, ICacheProvider cacheProvider)
        {
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            this.cacheProvider = cacheProvider ?? throw new ArgumentNullException(nameof(cacheProvider));
        }

        public IReadOnlyList<Category> ReadCategories()
        {
            if (cacheProvider.CategoryCache == null)
            {
                cacheProvider.CategoryCache = categoryRepository.ReadCategories();
            }
            return cacheProvider.CategoryCache;
        }
    }
}
