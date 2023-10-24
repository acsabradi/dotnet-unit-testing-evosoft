using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model
{
    // NOTE: Ez a fájl valójában nem része a forráskódnak, a NAVtól kaptuk egy SDK részeként
    public interface INavContext : IDisposable
    {
        void SendFile(string fileName, string userName);
    }
    public class NavContext : INavContext
    {
        public NavContext()
        {
            Console.WriteLine("Opening network connection to NAV services");
        }
        public void SendFile(string fileName, string userName)
        {
            using (var ms = new StreamReader(fileName))
            {
                string content = ms.ReadToEnd();
                Console.WriteLine($"Sending invoice on behalf of {userName}");
            }
        }
        public void Dispose()
        {
            Console.WriteLine("Closing NAV connection");
        }
    }
}
