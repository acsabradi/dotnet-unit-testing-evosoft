using System;

namespace Inventory.Model
{
    internal class DateProvider : IDateProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
