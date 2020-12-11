using Lab4.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using Lab3.Core;

namespace Lab4.Commands
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
                var rc5 = new Rc5CbcPad();
                using var input = new BinaryReader(File.OpenRead(vm.FilePath));
                using var output = new BinaryWriter(File.Create(dialog.FileName));
                rc5.Encrypt(input, vm.KeyPhrase, output);
            }
        }
    }
}