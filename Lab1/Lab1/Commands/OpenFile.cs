using System;
using System.Windows.Input;
using Lab1.ViewModels;
using Microsoft.Win32;

namespace Lab1.Commands
{
    class OpenFile : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var input = (InputParametersViewModel)parameter;

            OpenFileDialog dialog = new OpenFileDialog()
            {
                AddExtension = true,
                CheckFileExists = true,
                Filter = "Text documents (.txt)|*.txt",
                DefaultExt = "txt",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };

            if (dialog.ShowDialog().HasValue)
            {
                input.FilePath = dialog.FileName;
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
