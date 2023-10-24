using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsItChristmasDemo
{
    public interface IDateProvider
    {
        DateTime Now { get; }
    }

    public class DateProvider : IDateProvider
    {
        public DateTime Now => DateTime.Now;
    }

    public class OtherChristmasService
    {
        private readonly IIsItChristmasService isItChristmasService;

        public OtherChristmasService(IIsItChristmasService isItChristmasService)
        {
            this.isItChristmasService = isItChristmasService ?? throw new ArgumentNullException(nameof(isItChristmasService));
        }

        public void M() { isItChristmasService.IsIt(); }
    }


    public class IsItChristmasService2 : IIsItChristmasService
    {
        private readonly IDateProvider dateProvider;
        private readonly IChristmasOutputService outputService;

        public IsItChristmasService2(IDateProvider dateProvider, IChristmasOutputService outputService)
        {
            this.dateProvider = dateProvider ?? throw new ArgumentNullException(nameof(dateProvider));
            this.outputService = outputService ?? throw new ArgumentNullException(nameof(outputService));
        }

        private bool IsItChristmas(DateTime now)
        {
            
            return now.Month == 12 && (now.Day == 25 || now.Day == 26);
        }

        public void IsIt()
        {
            var now = dateProvider.Now; //DateTime.Now;
            if (IsItChristmas(now))
            {
                outputService.WriteChristmas();
            }
            else
            {
                outputService.WriteNotChristmas();
            }
        }
    }

    public class IsItChristmasService : IIsItChristmasService
    {
        private readonly IDateProvider dateProvider;
        private readonly IOutputService outputService;

        public IsItChristmasService(IDateProvider dateProvider, IOutputService outputService)
        {
            this.dateProvider = dateProvider ?? throw new ArgumentNullException(nameof(dateProvider));
            this.outputService = outputService ?? throw new ArgumentNullException(nameof(outputService));
        }

        private bool IsItChristmas(DateTime now)
        {

            return now.Month == 12 && (now.Day == 24 || now.Day == 25);
        }

        public void IsIt()
        {
            var now = dateProvider.Now; //DateTime.Now;
            if (IsItChristmas(now))
            {
                outputService.WriteLine("YES");
            }
            else
            {
                outputService.WriteLine("NO");
            }
        }
    }


    public interface IOutputService
    {
        void WriteLine(string message);
    }

    public class ConsoleOutputService : IOutputService
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FileOutputService : IOutputService
    {
        public void WriteLine(string message)
        {
            File.WriteAllText("output.txt", message);
        }
    }

    public interface IChristmasOutputService
    {
        void WriteChristmas();
        void WriteNotChristmas();
    }

    public class ConsoleChristmasOutputService : IChristmasOutputService
    {
        public void WriteChristmas()
        {
            Console.WriteLine("YES");
        }

        public void WriteNotChristmas()
        {
            Console.WriteLine("NO");
        }
    }


    public class FileChristmasOutputService : IChristmasOutputService
    {
        public void WriteChristmas()
        {
            File.WriteAllText("output.txt","YES");
        }

        public void WriteNotChristmas()
        {
            File.WriteAllText("output.txt","NO");
        }
    }

    public class DateProviderStub : IDateProvider
    {
        public DateTime Now => new DateTime(2023, 12, 26);
    }

    
    public class DateProviderMock: IDateProvider // builder pattern
    {
        public int Month { get; set; } // validate
        public int Day { get; set; } // validate
        public DateTime Now => new DateTime(2023, Month, Day);
    }


    public class TestOutputService : IOutputService
    {
        public List<string> Outputs { get; } = new List<string>();
        public void WriteLine(string message)
        {
            Outputs.Add(message);
        }
    }

    public class TestChristmasOutputService : IChristmasOutputService
    {
        public bool WriteChristmasCalled { get; private set; }
        public bool WriteNotChristmasCalled { get; private set; }

        public void WriteChristmas()
        {
            WriteChristmasCalled = true;
        }

        public void WriteNotChristmas()
        {
            WriteNotChristmasCalled = true;
        }
    }

    internal class Program
    {
        static void TestChristmas() // 12. 26.
        {
            DateProviderMock testDateProvider = new DateProviderMock { Month = 12, Day = 26 };
            TestOutputService outputService = new TestOutputService();
            TestChristmasOutputService testChristmasOutputService = new TestChristmasOutputService();
            IsItChristmasService isItChristmasService = new IsItChristmasService(testDateProvider, outputService);
            
            isItChristmasService.IsIt();

            if (outputService.Outputs.Single()=="YES")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("NOT");
            }

            //if (testChristmasOutputService.WriteChristmasCalled && !testChristmasOutputService.WriteNotChristmasCalled)
            //{
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine("OK");
            //}
            //else
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("NOT");
            //}
        }

        static void Main(string[] args)
        {
            TestChristmas();

            //IsItChristmasService isItChristmasService = new IsItChristmasService(new DateProvider(), new ConsoleOutputService());
            //isItChristmasService.IsIt();
            Console.ReadLine();
        }
    }
}
