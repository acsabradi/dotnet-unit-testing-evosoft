using Autofac;
using Inventory.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Inventory.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IContainer Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule<ViewModule>();
            Container = builder.Build();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Container?.Dispose();
        }
    }
}
