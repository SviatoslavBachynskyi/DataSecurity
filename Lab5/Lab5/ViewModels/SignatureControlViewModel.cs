using System.IO;
using System.Security.Cryptography;
using Lab5.Commands;

namespace Lab5.ViewModels
{
    public class SignatureControlViewModel : BaseViewModel
    {
        public MainWindowViewModel Main { get; set; }

        public SignatureControlViewModel(MainWindowViewModel main)
        {
            Main = main;
        }

        public SaveToFile SaveToFile { get; set; } = new SaveToFile();

        public LoadFile LoadFile { get; set; } = new LoadFile();
    }
}
