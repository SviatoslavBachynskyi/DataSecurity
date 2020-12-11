using Lab4.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using Lab3.Core;

namespace Lab4.Commands
{
    public class RsaExport : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var vm = (RsaViewModel)parameter;

            var dialog = new SaveFileDialog()
            {
                AddExtension = true,
                FileName = "PublicKey.public",
                Title = "Select file to save public key",
                DefaultExt = "public",
                InitialDirectory = vm.FilePath,
            };

            if (dialog.ShowDialog() ?? false)
            {
                File.WriteAllBytes(dialog.FileName, vm.CryptoService.ExportCspBlob(false));
            }

            dialog = new SaveFileDialog()
            {
                AddExtension = true,
                FileName = "PrivateKey.private",
                Title = "Select file to save private key",
                DefaultExt = "private",
                InitialDirectory = vm.FilePath,
            };

            if (dialog.ShowDialog() ?? false)
            {
                File.WriteAllBytes(dialog.FileName, vm.CryptoService.ExportCspBlob(true));
            }
        }
    }
}