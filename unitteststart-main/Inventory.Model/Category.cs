using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Inventory.Model
{
    [DataContract(Namespace = "CSharpHalado")]
    public class Category
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int CategoryID { get; set; }

        [DataMember]
        public string Description { get; set; }

        public List<Product> Products { get; set; }     
    }
}
