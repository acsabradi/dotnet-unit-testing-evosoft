using System;

namespace Inventory.Model
{
    public interface IDateProvider
    {
        DateTime Now { get; }
    }
}
