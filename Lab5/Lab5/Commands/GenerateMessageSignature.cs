using Lab5.ViewModels;

namespace Lab5.Commands
{
    public class GenerateMessageSignature : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var vm = ((TextViewModel)parameter).Main;

            vm.Generate.Execute(vm);
        }
    }
}

