using Lab5.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;

namespace Lab5.Commands
{
    public class SaveToFile : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var vm = (SignatureControlViewModel)parameter;

            return !String.IsNullOrEmpty(vm?.Main?.Signature);
        }

        public override void Execute(object parameter)
        {
            var vm = (SignatureControlViewModel)parameter;

            var dialog = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = "txt",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };

            if (dialog.ShowDialog() ?? false)
            {
                File.WriteAllBytes(dialog.FileName, Encoding.Unicode.GetBytes(vm.Main.Signature));
            }
        }
    }
}