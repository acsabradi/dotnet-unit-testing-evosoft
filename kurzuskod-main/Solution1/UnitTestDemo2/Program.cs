using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestDemo2
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


    public class PeopleService
    {
        private readonly IPersonNameWriter personNameWriter;

        public PeopleService(IPersonNameWriter personNameWriter)
        {
            this.personNameWriter = personNameWriter ?? throw new ArgumentNullException(nameof(personNameWriter));
        }

        public void Process(List<Person> people)
        {
            foreach (Person person in people)
            {
                if (person.Age < 18)
                    personNameWriter.AppendName(person.Name);
            }
        }
    }


    public interface IPersonNameWriter
    {
        void AppendName(string name);
    }

    public class FilePersonNameWriter : IPersonNameWriter
    {
        private string filePath;

        public FilePersonNameWriter(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public void AppendName(string name)
        {
            File.AppendAllLines(filePath, new[] { name });
        }
    }

    public class ConsolePersonNameWriter : IPersonNameWriter
    {
        public void AppendName(string name)
        {
            Console.WriteLine(name);
        }
    }

    public class TestPersonNameWriter : IPersonNameWriter
    {
        public List<string> Names { get; private set; }
        public void AppendName(string name)
        {
            Names.Add(name);
        }
    }

    internal class Program
    {
        static void UnitTest()
        {
            string adultName = "Akos3";
            var people = new List<Person>
            {
                new Person { Age = 16, Name="Akos" },
                new Person { Age = 17, Name="Akos2"},
                new Person { Age = 18, Name=adultName}
            };

            TestPersonNameWriter personNameWriter = new TestPersonNameWriter();
            PeopleService p = new PeopleService(personNameWriter);
            p.Process(people);

            if (personNameWriter.Names.Single() == adultName)
            {
                Console.WriteLine("OK");
            }
            else
            {
                throw new Exception("Errr");
            }
        }

        static void Main(string[] args)
        {
            var people = new List<Person>
            {
                new Person { Age = 16, Name="Akos" },
                new Person { Age = 17, Name="Akos2"},
                new Person { Age = 18, Name="Akos3"}
            };

            PeopleService p = new PeopleService(new FilePersonNameWriter("output.txt"));
            p.Process(people);
        }
    }
}
