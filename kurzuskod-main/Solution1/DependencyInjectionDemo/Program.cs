using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo
{
    public class MainComponent
    {
        private readonly IComponent component;

        public MainComponent(IComponent component)
        {
            this.component = component ?? throw new ArgumentNullException(nameof(component));
        }

        public void DoSystem() 
        {
            component.DoSubsystem();
        }
    }

    public interface IComponent
    {
        void DoSubsystem();
    }

    public class Component : IComponent
    {
        private readonly ISubcomponent subcomponent;

        public Component(ISubcomponent subcomponent)
        {
            this.subcomponent = subcomponent ?? throw new ArgumentNullException(nameof(subcomponent));
        }

        public void DoSubsystem()
        {
            subcomponent.DoSubsubsystem();
        }
    }

    public interface ISubcomponent
    {
        void DoSubsubsystem();
    }

    public class Subcomponent : ISubcomponent
    {
        private readonly ISubsubComponent subsubComponent;

        public Subcomponent(ISubsubComponent subsubComponent)
        {
            this.subsubComponent = subsubComponent ?? throw new ArgumentNullException(nameof(subsubComponent));
        }

        public void DoSubsubsystem()
        {
            subsubComponent.DoSubsubsubSystem();
        }
    }

    public interface ISubsubComponent
    {
        void DoSubsubsubSystem();
    }

    public class SubsubComponent : ISubsubComponent
    {
        public void DoSubsubsubSystem() { }
    }


   
    internal class Program
    {
        static void Main(string[] args)
        {
            // composition root

            // seam

            //MainComponent mc = new MainComponent(
            //    new Component(
            //        new Subcomponent(
            //            new SubsubComponent()
            //            )
            //        )
            //);

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<Component>().As<IComponent>();
            builder.RegisterType<Subcomponent>().As<ISubcomponent>().SingleInstance();
            builder.RegisterType<SubsubComponent>().As<ISubsubComponent>();
            builder.RegisterType<MainComponent>().As<MainComponent>();
            var container = builder.Build();

            var sc = container.Resolve<ISubcomponent>();
            var sc2 = container.Resolve<ISubcomponent>();

            Console.WriteLine(sc == sc2);

            Console.ReadLine();
        }
    }
}
