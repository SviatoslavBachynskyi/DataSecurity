using Lab4.ViewModels;
using Microsoft.Win32;
using System;

namespace Lab4.Commands
{
    public class Rc5OpenFile : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var vm = (Rc5ViewModel)parameter;

            OpenFileDialog dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };

            if (dialog.ShowDialog() ?? false)
            {
                vm.FilePath = dialog.FileName;
            }
        }
    }
}