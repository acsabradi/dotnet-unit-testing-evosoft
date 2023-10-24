using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model
{
    internal class Logger : ILogger
    {
        public void LogInformation(string information)
        {
            Console.WriteLine(information);
        }
    }
}
