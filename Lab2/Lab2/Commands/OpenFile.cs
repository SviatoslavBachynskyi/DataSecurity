using Lab2.ViewModels;
using Microsoft.Win32;
using System;

namespace Lab2.Commands
{
    public class OpenFile : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var vm = (FileHashingViewModel)parameter;

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