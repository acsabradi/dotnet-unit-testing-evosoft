using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversion
{
    public class A
    {
        private IB field;

        public A(IB field)
        {
            this.field = field ?? throw new ArgumentNullException(nameof(field));
        }


        public IB field2;

        public void M(IB param) {  }
    }

    public interface IB { void M(); }
    public class B : IB
    {
        public void M() { }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            new List<A>().Sort(new AComparer());

            A a = new A(new B());
            a.field2 = new B();

            a.M(new B());
        }
    }

    public class AComparer : IComparer<A>
    {
        public int Compare(A x, A y)
        {
            throw new NotImplementedException();
        }
    }
}
