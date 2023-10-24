using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerDemo
{
    public class Dal : IDisposable, IDal
    {
        public Dal()
        {
            Console.WriteLine("Open connection to database");
        }

        public void Dispose()
        {
            Console.WriteLine("Closing connection to database");
        }

        public int GetDataFromDatabase()
        {
            Console.WriteLine("SELECT COUNT(*) FROM USERS");
            return new Random().Next();
        }
    }

    public interface IDal
    {
        int GetDataFromDatabase();
    }

    public class Bll
    {
        private readonly IDal dal;
        public Bll(IDal dal)
        {
            this.dal = dal ?? throw new ArgumentNullException(nameof(dal));
        }

        public int DoBusiness()
        {
            var userData = dal.GetDataFromDatabase();
            if (userData % 2 == 0)
                return userData / 3;

            return userData / 5;
        }
    }


    public class TestDal : IDal
    {
        public int ReturnValue { get; set; }

        public int GetDataFromDatabase()
        {
            return ReturnValue;
        }
    }


    public class DalProxy : IDal
    {
        public int GetDataFromDatabase()
        {
            using (Dal dal = new Dal()) 
                return dal.GetDataFromDatabase();
        }
    }

    internal class Program
    {
        static void UnitTest()
        {
            Bll bll = new Bll(new TestDal());
            if (bll.DoBusiness() == 3)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("Errr");
            }
        }
        static void Main(string[] args)
        {
            //UnitTest();

            Bll bll = new Bll(new DalProxy());
            int j = bll.DoBusiness();
            int j2 = bll.DoBusiness();
            Console.WriteLine(j);

            Console.ReadLine();
        }
    }
}
