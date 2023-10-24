namespace Inventory.ViewModel
{
    public interface IDialog
    {
        bool? ShowDialog();
        string FileName { get; }
    }
}
