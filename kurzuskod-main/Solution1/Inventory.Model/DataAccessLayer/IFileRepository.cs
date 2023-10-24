using System.Collections.Generic;

namespace Inventory.Model
{
    public interface IFileRepository
    {
        IReadOnlyList<T> Read<T>(string filePath);
    }
}
