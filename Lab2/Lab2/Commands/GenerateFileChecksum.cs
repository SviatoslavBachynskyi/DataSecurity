using Lab2.Core;
using Lab2.ViewModels;
using System.IO;

namespace Lab2.Commands
{
    public class GenerateFileChecksum : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var vm = (FileHashingViewModel)parameter;

            return !string.IsNullOrEmpty(vm?.FilePath);
        }

        public override void Execute(object parameter)
        {
            var vm = (FileHashingViewModel)parameter;

            var hasher = new Md5HashGenerator();

            vm.GeneratedChecksum = hasher.Hash(new BinaryReader(File.OpenRead(vm.FilePath)));
        }
    }
}

