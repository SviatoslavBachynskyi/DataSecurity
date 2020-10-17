using System;
using System.IO;
using System.Windows.Input;
using Lab1.Commands;

namespace Lab1.ViewModels
{
    public class InputParametersViewModel : BaseViewModel
    {
        public string X0 { get; set; }
        
        public string A { get; set; }
        
        public string C { get; set; }
        
        public string M { get; set; }
     
        public string Count { get; set; }

        public string FilePath { get; set; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}{Path.DirectorySeparatorChar}lab1.txt";

        public ICommand OpenFile { get; set; } = new OpenFile();

        public ICommand WriteToConsole { get; set; } = new Write(true);

        public ICommand WriteToFile { get; set; } = new Write();
    }
}
