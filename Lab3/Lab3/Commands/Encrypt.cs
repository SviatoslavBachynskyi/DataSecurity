using Lab3.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;

namespace Lab3.Commands
{
    public class Encrypt : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var vm = (MainWindowViewModel)parameter;

            return !String.IsNullOrEmpty(vm?.FilePath);
        }

        public override void Execute(object parameter)
        {
            var vm = (MainWindowViewModel)parameter;

            var dialog = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = Path.GetExtension(vm.FilePath),
                InitialDirectory = vm.FilePath,
            };

            if (dialog.ShowDialog() ?? false)
            {

            }
        }
    }
}