namespace Inventory.Model
{
    public interface IApplicationServices
    {
        IUserProvider UserProvider { get; }
        ILogger Logger { get; }
        IDateProvider DateProvider { get; }
    }
}
