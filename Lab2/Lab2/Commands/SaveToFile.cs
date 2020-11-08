using Lab2.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;

namespace Lab2.Commands
{
    public class SaveTextChecksumToFile : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var vm = (TextHashingViewModel)parameter;

            return !String.IsNullOrEmpty(vm?.Checksum);
        }

        public override void Execute(object parameter)
        {
            var vm = (TextHashingViewModel)parameter;

            var dialog = new SaveFileDialog()
            {
                AddExtension = true,
                Filter = "Text document (.txt)|*.txt",
                DefaultExt = "txt",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };

            if (dialog.ShowDialog() ?? false)
            {
                File.WriteAllText(dialog.FileName, $"{vm.Checksum}{Environment.NewLine}");
            }
        }
    }
}