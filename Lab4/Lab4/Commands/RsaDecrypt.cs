using Lab4.ViewModels;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using Lab3.Core;

namespace Lab4.Commands
{
    public class RsaDecrypt : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var vm = (RsaViewModel)parameter;

            return !String.IsNullOrEmpty(vm?.FilePath);
        }

        public override void Execute(object parameter)
        {
            var vm = (RsaViewModel)parameter;

            var dialog = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = Path.GetExtension(vm.FilePath),
                InitialDirectory = vm.FilePath,
            };

            if (dialog.ShowDialog() ?? false)
            {
                var sw = Stopwatch.StartNew();
                using var input = new BinaryReader(File.OpenRead(vm.FilePath));
                using var output = new BinaryWriter(File.Create(dialog.FileName));
                var buffer = new byte[RsaViewModel.DecryptBlockLength];
                int bytesRead = 0;

                do
                {
                    bytesRead = input.Read(buffer);

                    if (bytesRead == 0)
                        continue;

                    var decrypted = vm.CryptoService.Decrypt(buffer, false);
                    output.Write(decrypted);

                } while (bytesRead == RsaViewModel.DecryptBlockLength);

                sw.Stop();

                vm.DecryptionTime = $"Decryption took {sw.ElapsedMilliseconds} ms";
            }
        }
    }
}