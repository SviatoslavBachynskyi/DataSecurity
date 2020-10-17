using Lab1.ViewModels;
using System;
using System.IO;
using System.Text;
using System.Windows.Input;
using Lab1.Core;

namespace Lab1.Commands
{
    public class Write : ICommand
    {
        private bool _toConsole;

        public Write(bool toConsole = false)
        {
            _toConsole = toConsole;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var mainWindowViewModel = (MainWindowViewModel)parameter;
            var input = mainWindowViewModel.InputParametersViewModel;
            var output = mainWindowViewModel.OutputViewModel;

            var lcParameter = new LcConstants
            (
                uint.Parse(input.X0),
                uint.Parse(input.A),
                uint.Parse(input.C),
                uint.Parse(input.M)
            );

            var stringBuilder = new StringBuilder();
            var stringWriter = new StringWriter(stringBuilder);
            var streamWriter = new StreamWriter(input.FilePath);

            var period = LcPseudoRandomGenerator.Generate
            (
                lcParameter,
                streamWriter,
                _toConsole ? stringWriter : null,
                uint.Parse(input.Count)
            );

            output.Period = period.ToString();
            output.GeneratedSequence = _toConsole ? stringBuilder.ToString() : "";

            streamWriter.Close();
        }

    }
}
