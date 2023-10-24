namespace Inventory.Model
{
    internal class NavContextFactory : INavContextFactory
    {
        public INavContext CreateNavContext()
        {
            return new NavContext();
        }
    }
}
