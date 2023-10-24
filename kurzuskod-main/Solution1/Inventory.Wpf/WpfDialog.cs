using Inventory.ViewModel;
using Microsoft.Win32;

namespace Inventory.Wpf
{
    public class WpfDialog : IDialog
    {
        private readonly SaveFileDialog dialog;
        public WpfDialog()
        {
            this.dialog = new SaveFileDialog();
        }
        public string FileName => dialog.FileName;

        public bool? ShowDialog()
        {
            return dialog.ShowDialog();
        }
    }
}
