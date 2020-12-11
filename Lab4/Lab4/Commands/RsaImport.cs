using Lab4.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using Lab3.Core;

namespace Lab4.Commands
{
    public class RsaImport : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var vm = (RsaViewModel)parameter;

            var dialog = new OpenFileDialog()
            {
                AddExtension = true,
                FileName = "PrivateKey.private",
                Title = "Select file to import private key",
                DefaultExt = "private",
                InitialDirectory = vm.FilePath,
            };

            if (dialog.ShowDialog() ?? false)
            {
                vm.CryptoService.ImportCspBlob(File.ReadAllBytes(dialog.FileName));
            }
        }
    }
}