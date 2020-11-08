using Lab2.Core;
using Lab2.ViewModels;

namespace Lab2.Commands
{
    public class GenerateMessageChecksum : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var vm = (TextHashingViewModel)parameter;

            var hasher = new Md5HashGenerator();

            vm.Checksum = hasher.Hash(vm.Message);
        }
    }
}

