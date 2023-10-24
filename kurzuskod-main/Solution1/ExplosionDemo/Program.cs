using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExplosionDemo
{

    public interface IExplosiveTimerFactory
    {
        IExplosiveTimer CreateTimer();
    }

    public class ExplosiveTimerFactory : IExplosiveTimerFactory
    {
        public IExplosiveTimer CreateTimer()
        {
            return new ExplosiveTimer();
        }
    }

    public class ExplosiveDevice
    {
        private readonly IExplosiveTimerFactory explosiveTimerFactory;

        public ExplosiveDevice(IExplosiveTimerFactory explosiveTimerFactory)
        {
            this.explosiveTimerFactory = explosiveTimerFactory ?? throw new ArgumentNullException(nameof(explosiveTimerFactory));
        }

        public void Trigger()
        {
            Console.WriteLine("Danger");
            IExplosiveTimer ownTimer = explosiveTimerFactory.CreateTimer();

            //IExplosiveTimer timer = new ExplosiveTimer();
        }
    }

    public interface IExplosiveTimer
    {
    }
    public class ExplosiveTimer : IExplosiveTimer
    {
        public ExplosiveTimer()
        {
            Timer t = new Timer(_ => Console.WriteLine("Tick-tick"), null, 0, 1000);
        }
    }

    public class NullExplosiveTimerFactory : IExplosiveTimerFactory
    {
        public IExplosiveTimer CreateTimer()
        {
            return new NullExplosiveTimer();
        }
    }

    public class NullExplosiveTimer : IExplosiveTimer
    {
     
    }

    internal class Program
    {

        static void UnitTest()
        {
            ExplosiveDevice d = new ExplosiveDevice(new NullExplosiveTimerFactory());
            d.Trigger();
        }

        static void Main(string[] args)
        {
            //UnitTest();

            ExplosiveTimerFactory explosiveTimerFactory = new ExplosiveTimerFactory();
            ExplosiveDevice ed = new ExplosiveDevice(explosiveTimerFactory);
            ed.Trigger();

            ExplosiveDevice ed2 = new ExplosiveDevice(explosiveTimerFactory);
            ed2.Trigger();

            Console.ReadLine();
        }
    }
}
