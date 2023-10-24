using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLayerDemo2
{
    public class Bll
    {
        private readonly IDal dal;

        public Bll(IDal dal)
        {
            this.dal = dal ?? throw new ArgumentNullException(nameof(dal));
        }

        public void DoBusiness()
        {
            var x = new Random();
            dal.SaveData(x.Next());
        }
    }

    public interface IDal
    {
        void SaveData(int j);
    }

    public class Dal : IDal
    {
        public void SaveData(int j)
        {
            Console.WriteLine("INSERT INTO");
        }
    }


    public class DalDecorator:IDal
    {
        private readonly IDal dal;

        public DalDecorator(IDal dal)
        {
            this.dal = dal ?? throw new ArgumentNullException(nameof(dal));
        }

        public void SaveData(int j)
        {
            var sw = Stopwatch.StartNew();
            this.dal.SaveData(j);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }

    public class PerformanceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var sw = Stopwatch.StartNew();
            invocation.Proceed();
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            ProxyGenerator generator = new ProxyGenerator();
            IDal decorator = generator.CreateInterfaceProxyWithTarget<IDal>(new Dal(), new PerformanceInterceptor());

            Bll bll = new Bll(decorator);
            bll.DoBusiness();
        }
    }
}
