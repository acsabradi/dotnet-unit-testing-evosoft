using Autofac;
using Inventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModel
{
    public class ViewModelModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterModule<ModelModule>();
        }
    }
}
