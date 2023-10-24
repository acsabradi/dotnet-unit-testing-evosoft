using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoqDemo
{
    public class Component // module under test
    {
        private readonly IDependency dependency;

        public Component(IDependency dependency)
        {
            this.dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
        }

        public void M()
        {
            dependency.DoSomething();
        }
    }

    public interface IDependency
    {
        int DoSomething();
        int DoSomething2(string t);
        void DoSomething3();
        int X { get; set; }
        void HackPerson(Person person);
    }

    public class Person { public int Age { get; set; } }

    public interface IDependency2
    {
        IDependency X { get; }
    }


    public abstract class DependencyBase
    {
        public abstract void M();
        public void M2() { }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            //Mock<DependencyBase> m = new Mock<DependencyBase>();
            //m.Setup(x => x.M()).Callback(() => { });
            ////m.Setup(x => x.M2()).Callback(() => { });

            //m.Object.M();
            //m.Object.M2();

            //Mock<IDependency2> dep2Mock = new Mock<IDependency2>() { DefaultValue = DefaultValue.Mock };   
            //Mock.Get(dep2Mock.Object.X).Setup(dep=>dep.DoSomething()).Returns(30);
            //Mock.Get(dep2Mock.Object.X).Setup(dep => dep.HackPerson(It.IsAny<Person>()))
            //                            .Callback<Person>((p) => p.Age++);

            //IDependency d = dep2Mock.Object.X;


            //Mock<IDependency> depMock = new Mock<IDependency>();
            //depMock.Setup(d => d.DoSomething()).Returns(14);

            //Mock<IDependency2> dep2Mock = new Mock<IDependency2>();
            //dep2Mock.SetupGet(dep2 => dep2.X).Returns(depMock.Object);

            //IDependency x = dep2Mock.Object.X;
            //int t = x.DoSomething();


            //Mock<IDependency> dependencyMock = new Mock<IDependency>(MockBehavior.Strict);
            //dependencyMock.Setup(d => d.DoSomething())
            //              .Returns(0);

            //dependencyMock.Setup(d => d.DoSomething3());

            //int j = dependencyMock.Object.DoSomething();
            //dependencyMock.Object.DoSomething3();

            //int calls = 0;
            //dependencyMock.Setup(dep => dep.DoSomething())
            //              .Callback(() => calls++)
            //              .Returns(() => calls);

            //dependencyMock.Setup(dep => dep.DoSomething())
            //              .Returns(42);

            //dependencyMock.Setup(dep=>dep.DoSomething()).Throws<ArgumentException>();

            //dependencyMock.Setup(dep => dep.DoSomething2("Akos")).Returns(50);
            //dependencyMock.Setup(dep => dep.DoSomething2("Bela")).Throws<ArgumentException>();

            //dependencyMock.Setup(dep => dep.DoSomething2(It.IsAny<string>())).Returns(40);

            //IDependency dependency = dependencyMock.Object;
            //var result = dependency.DoSomething2("Bela");


            //dependencyMock.Verify(dep=>dep.DoSomething2("Akos"),Times.Once());
            //dependencyMock.Verify(dep=>dep.DoSomething2(It.Is<string>(s=>s.Length>4)),Times.Once());
            //dependencyMock.Verify(dep=>dep.DoSomething2(It.IsAny<string>()),Times.Once());

            //dependencyMock.Setup(dep => dep.DoSomething()).Callback(() => Console.WriteLine("Meghívtak"));
            //dependencyMock.Setup(dep => dep.DoSomething2(It.IsAny<string>())).Callback((string s) => Console.WriteLine(s));
            //dependencyMock.SetupGet(d => d.X).Returns(42);
            //dependencyMock.SetupSet(dep=>dep.X).Callback((int value)=> Console.WriteLine(value));

            //dependencyMock.SetupProperty(d => d.X);
            //dependencyMock.SetupAllProperties();

            //dependencyMock.Object.X = 40;
            //Console.WriteLine(dependencyMock.Object.X);

        }
    }
}
