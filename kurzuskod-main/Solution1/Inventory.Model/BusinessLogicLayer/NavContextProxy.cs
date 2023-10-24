using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model
{
    internal class NavContextProxy : INavContext
    {
        public void Dispose()
        {
        }

        public void SendFile(string fileName, string userName)
        {
            using (NavContext navContext = new NavContext())
                navContext.SendFile(fileName, userName);
        }
    }
}
