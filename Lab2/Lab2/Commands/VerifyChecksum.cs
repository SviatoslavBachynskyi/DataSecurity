using Lab2.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;

namespace Lab2.Commands
{
    public class VerifyChecksum : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var vm = (FileHashingViewModel)parameter;

            return !String.IsNullOrEmpty(vm?.GeneratedChecksum);
        }

        public override void Execute(object parameter)
        {
            var vm = (FileHashingViewModel)parameter;

            OpenFileDialog dialog = new OpenFileDialog()
            {
                AddExtension = true,
                CheckFileExists = true,
                Filter = "Md5 document (.md5)|*.md5",
                DefaultExt = "md5",
                InitialDirectory = vm.FilePath,
            };

            if (dialog.ShowDialog() ?? false)
            {

                var buffer = new char[32];
                
                new StreamReader(dialog.FileName).ReadBlock(buffer);

                vm.LoadedChecksum = new string(buffer);
            }
        }
    }
}