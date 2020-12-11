using System;
using System.Windows.Input;

namespace Lab4.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());

        }
    }
}
