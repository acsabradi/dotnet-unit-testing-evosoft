using Inventory.ViewModel;

namespace Inventory.Wpf
{
    public class WpfDialogFactory : IDialogFactory
    {
        public IDialog CreateDialog()
        {
            return new WpfDialog();
        }
    }
}
