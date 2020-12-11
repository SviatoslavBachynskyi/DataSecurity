using System;
using System.Collections.Generic;
using System.Text;
using Lab4.Commands;

namespace Lab4.ViewModels
{
    public class Rc5ViewModel : BaseViewModel
    {
        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
                Rc5Encrypt.OnCanExecuteChanged();
                Rc5Decrypt.OnCanExecuteChanged();
            }
        }

        public Rc5OpenFile Rc5OpenFile { get; set; } = new Rc5OpenFile();

        public Rc5Decrypt Rc5Decrypt { get; set; } = new Rc5Decrypt();

        public Rc5Encrypt Rc5Encrypt { get; set; } = new Rc5Encrypt();

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

        private string _encryptionTime;

        public string EncryptionTime
        {
            get => _encryptionTime;
            set
            {
                _encryptionTime = value;
                OnPropertyChanged(nameof(EncryptionTime));
            }
        }

        private string _decryptionTime;

        public string DecryptionTime
        {
            get => _decryptionTime;
            set
            {
                _decryptionTime = value;
                OnPropertyChanged(nameof(DecryptionTime));
            }
        }
    }
}
