using Lab5.ViewModels;
using System.IO;

namespace Lab5.Commands
{
    public class GenerateFileSignature : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var vm = (FileViewModel)parameter;

            return !string.IsNullOrEmpty(vm?.FilePath);
        }

        public override void Execute(object parameter)
        {
            var vm = ((FileViewModel)parameter).Main;

            vm.Generate.Execute(vm);
        }
    }
}

