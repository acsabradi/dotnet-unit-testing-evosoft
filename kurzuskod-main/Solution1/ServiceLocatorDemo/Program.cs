using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLocatorDemo
{
    public class Locator
    {
        private static readonly Dictionary<Type, Type> registrations = new Dictionary<Type, Type>();

        public static void Register<TInterface,TImplementation>() where TImplementation:TInterface
        {
            registrations.Add(typeof(TInterface), typeof(TImplementation));
        }

        public static TInterface Get<TInterface>()
        {
            return (TInterface)Activator.CreateInstance(registrations[typeof(TInterface)]);
        }
    }


    public class ProductService
    {
        private readonly IProductRepository productRepository;
        public ProductService()
        {
            productRepository = Locator.Get<IProductRepository>();
        }
    }

    public class ProductService2
    {
        private readonly IProductRepository productRepository;

        public ProductService2(IProductRepository productRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
    }


    public interface IProductRepository
    {
        void SaveData();
    }

    public class ProductRepository : IProductRepository
    {
        public void SaveData()
        {
            Console.WriteLine("INSERT INTO");
        }
    }

    public class TestProductRepository : IProductRepository
    {
        public void SaveData()
        {
            Console.WriteLine("In memory");
        }
    }

    internal class Program
    {
        static void UnitTest()
        {
            ProductService ps = new ProductService();
        }

        static void UnitTest2()
        {
            // ProductService2 ps = new ProductService2();
        }

        static void Main(string[] args)
        {
            //Locator.Register<IProductRepository,ProductRepository>();
            //ProductService ps = new ProductService();

            UnitTest();
        }
    }
}
