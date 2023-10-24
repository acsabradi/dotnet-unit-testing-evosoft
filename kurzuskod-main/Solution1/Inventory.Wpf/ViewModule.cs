using Autofac;
using Inventory.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Wpf
{
    public class ViewModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule<ViewModelModule>();
            builder.RegisterType<WpfDialogFactory>().As<IDialogFactory>();
        }
    }
}
