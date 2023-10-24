using System.Security.Principal;

namespace Inventory.Model
{
    public interface IUserProvider
    {
        IIdentity User { get; }
    }
}
