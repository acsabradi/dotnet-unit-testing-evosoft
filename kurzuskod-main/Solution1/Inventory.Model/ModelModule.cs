using Autofac;
using Autofac.Extras.AggregateService;
using Inventory.Model.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model
{
    public class ModelModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<CacheProvider>().As<ICacheProvider>().SingleInstance();

            builder.RegisterDecorator<CachingProductRepository, IProductRepository>();
            builder.RegisterDecorator<CachingCategoryRepository, ICategoryRepository>();

            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<DateProvider>().As<IDateProvider>();
            builder.RegisterType<UserProvider>().As<IUserProvider>();

            builder.RegisterAggregateService<IApplicationServices>();
            //builder.RegisterType<ApplicationServices>().As<IApplicationServices>();

            builder.RegisterType<NavContextFactory>().As<INavContextFactory>();
            builder.RegisterType<ProductService>().As<IProductService>();

            builder.RegisterType<FileInvoiceWriter>().As<IFileInvoiceWriter>();
        }
    }
}
