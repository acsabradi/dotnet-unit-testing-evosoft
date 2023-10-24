using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModel
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string QuantityPerUnit { get; set; }        
    }
}
