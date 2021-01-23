using System.IO;
using System.Security.Cryptography;
using Lab5.Commands;

namespace Lab5.ViewModels
{
    public class FileViewModel : BaseViewModel
    {
        public MainWindowViewModel Main { get; set; }

        private SHA1 _sha1 = SHA1.Create();

        public FileViewModel(MainWindowViewModel main)
        {
            Main = main;
        }

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
                Main.Hash = 
                _sha1.ComputeHash(File.OpenRead(_filePath));
                Generate.OnCanExecuteChanged();
            }
        }

        public OpenFile OpenFile { get; set; } = new OpenFile();

        public GenerateFileSignature Generate { get; set; } = new GenerateFileSignature();
    }
}
