using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.ViewModel
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke(parameter) ?? true;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = default(Func<object,bool>))
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
