using Lab4.ViewModels;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using Lab3.Core;

namespace Lab4.Commands
{
    public class RsaEncrypt : BaseCommand
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

                using var input = new BinaryReader(File.OpenRead(vm.FilePath));
                using var output = new BinaryWriter(File.Create(dialog.FileName));
                var buffer = new byte[RsaViewModel.EncryptBlockLength];
                int bytesRead = 0;

                var sw = Stopwatch.StartNew();
                do
                {
                    bytesRead = input.Read(buffer);

                    if (bytesRead == 0)
                        continue;
                    if (bytesRead != RsaViewModel.EncryptBlockLength)
                    {
                        var lastBlock = new byte[bytesRead];
                        Buffer.BlockCopy(buffer, 0, lastBlock, 0, bytesRead);
                        buffer = lastBlock;
                    }

                    var encrypted = vm.CryptoService.Encrypt(buffer, false);
                    output.Write(encrypted);

                } while (bytesRead == RsaViewModel.EncryptBlockLength);

                sw.Stop();

                vm.EncryptionTime = $"Encryption took {sw.ElapsedMilliseconds} ms";

            }
        }
    }
}