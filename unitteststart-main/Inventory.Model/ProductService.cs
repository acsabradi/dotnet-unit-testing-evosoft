using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Inventory.Model
{
    public class ProductService
    {
        private IReadOnlyList<Category> categoryCache;
        private IReadOnlyList<Product> productCache;
        private readonly Logger logger;

        public ProductService()
        {
            logger = new Logger();
        }

        private IReadOnlyList<Product> ReadProducts()
        {
            using (var prodFile = XmlReader.Create("products.xml"))
            {
                var dcs = new DataContractSerializer(typeof(List<Product>));
                var products = (List<Product>)dcs.ReadObject(prodFile);
                foreach (var product in products)
                {
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                    {
                        product.UnitPrice = product.UnitPrice * 0.95M;
                    }
                }
                logger.LogInformation("Products read");
                return products;
            }
        }

        private IReadOnlyList<Category> ReadCategories()
        {
            List<Category> categories = null;
            using (var catFile = XmlReader.Create("categories.xml"))
            {
                var dcs = new DataContractSerializer(typeof(List<Category>));
                categories = (List<Category>)dcs.ReadObject(catFile);
                foreach (var c in categories)
                {
                    c.Products = new List<Product>();
                }
            }
            logger.LogInformation("Categories read");
            return categories;
        }



        public IReadOnlyList<Category> GetCategoriesMaster()
        {
            if (categoryCache == null)
            {
                var products = ReadProducts().Where(p => !p.Discontinued).ToList();
                var categories = ReadCategories();
                foreach (var product in products)
                {
                    product.Category = categories.Single(c => c.CategoryID == product.CategoryID);
                    product.Category.Products.Add(product);
                }
                categoryCache = categories;
                productCache = products;
            }
            return categoryCache;
        }

        public void CreateInvoce(string productName, int requiredCount, string fileName)
        {
            var product = productCache.Single(p => p.Name == productName);
            var price = product.UnitPrice * (DateTime.Now.DayOfWeek == DayOfWeek.Friday ? 0.95M : 1M) * requiredCount * 1.27M;
            File.WriteAllText(fileName, $"{productName}\t{requiredCount}\t{product.QuantityPerUnit}\t{price}");
            File.AppendAllLines(fileName, new[] { DateTime.Now.ToString() });
            SendFileToNav(fileName);
            logger.LogInformation("Invoice sent");
        }

        private void SendFileToNav(string fileName)
        {

            using (var navContext = new NavContext())
            {
                navContext.SendFile(fileName, System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            }
        }
    }
}
