using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace Inventory.Model
{
    internal abstract class XmlFileRepository : IFileRepository
    {
        public IReadOnlyList<T> Read<T>(string filePath)
        {
            using (var sourceFile = XmlReader.Create(filePath))
            {
                var dcs = new DataContractSerializer(typeof(List<T>));
                var data = (List<T>)dcs.ReadObject(sourceFile);
                return data;
            }
        }
    }
}
