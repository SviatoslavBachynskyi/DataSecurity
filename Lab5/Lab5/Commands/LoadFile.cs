using Lab5.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;

namespace Lab5.Commands
{
    public class LoadFile : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var vm = (SignatureControlViewModel)parameter;

            OpenFileDialog dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };

            if (dialog.ShowDialog() ?? false)
            {
                vm.Main.Signature = Encoding.Unicode.GetString(File.ReadAllBytes(dialog.FileName));
            }
        }
    }
}