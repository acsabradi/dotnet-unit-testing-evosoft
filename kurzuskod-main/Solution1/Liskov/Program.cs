using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liskov
{

    public class Component
    {
        public virtual void M(int j)
        {
            if (j < 0)
                throw new ArgumentOutOfRangeException();

            Console.WriteLine(j);
        }
    }

    public class SpecialComponent : Component
    {
        public override void M(int j)
        {
            if (j < -3)
                throw new ArgumentException();

            //if (j < 3)
            //    throw new ArgumentException();


            // TODO
        }
    }

    public class SpecialComponent2 : Component
    {
        public override void M(int j)
        {
            if (j < 3)
                throw new ArgumentOutOfRangeException();

            // TODO: X
        }
    }

    internal class Program
    {
        static void TestComponentOutOfRange(Component component)
        {
            try
            {
                int minMinus = -1;
                component.M(minMinus);
                throw new InvalidOperationException("Test failed");
            }
            catch (ArgumentException)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK");
            }
        }

        static void Main(string[] args)
        {
            //Component x = new Component();
            //TestComponentOutOfRange(x);


            SpecialComponent2 y = new SpecialComponent2();
            TestComponentOutOfRange(y);

            Console.ReadLine();
        }
    }
}
