using Lab2.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;

namespace Lab2.Commands
{
    public class SaveToFile : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var vm = (FileHashingViewModel)parameter;

            return !String.IsNullOrEmpty(vm?.GeneratedChecksum);
        }

        public override void Execute(object parameter)
        {
            var vm = (FileHashingViewModel)parameter;

            var fileName = Path.GetFileName(vm.FilePath);

            var dialog = new SaveFileDialog()
            {
                AddExtension = true,
                Filter = "Md5 document (.md5)|*.md5",
                DefaultExt = "md5",
                FileName = Path.GetFileNameWithoutExtension(fileName),
                InitialDirectory = vm.FilePath,
            };

            if (dialog.ShowDialog() ?? false)
            {
                File.WriteAllText(dialog.FileName, $"{vm.GeneratedChecksum} *{fileName}{Environment.NewLine}");
            }
        }
    }
}