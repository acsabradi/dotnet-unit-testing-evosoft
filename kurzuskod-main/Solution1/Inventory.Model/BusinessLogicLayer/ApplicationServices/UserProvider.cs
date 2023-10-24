using System.Security.Principal;

namespace Inventory.Model
{
    internal class UserProvider : IUserProvider
    {
        public IIdentity User => WindowsIdentity.GetCurrent();
    }
}
