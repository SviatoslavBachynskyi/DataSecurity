using System;
using Lab5.ViewModels;
using System.IO;

namespace Lab5.Commands
{
    public class Generate : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var vm = (MainWindowViewModel)parameter;

            return vm?.Hash != null && vm.Hash.Length > 0;
        }

        public override void Execute(object parameter)
        {
            var vm = (MainWindowViewModel)parameter;

            vm.Signature = Convert.ToBase64String(vm.Dsa.CreateSignature(vm.Hash));
        }
    }
}

