using Lab4.Commands;

namespace Lab4.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
                Encrypt.OnCanExecuteChanged();
                Decrypt.OnCanExecuteChanged();
            }
        }

        public OpenFile OpenFile { get; set; } = new OpenFile();

        public Decrypt Decrypt { get; set; } = new Decrypt();

        public Encrypt Encrypt { get; set; } = new Encrypt();

        private string _keyPhrase;

        public string KeyPhrase
        {
            get => _keyPhrase;
            set
            {
                _keyPhrase = value;
                OnPropertyChanged(nameof(KeyPhrase));
            }
        }
    }
}
