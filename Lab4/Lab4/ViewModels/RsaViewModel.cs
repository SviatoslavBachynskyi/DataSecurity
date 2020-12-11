using System;
using System.Collections.Generic;
using System.Text;
using Lab4.Commands;

namespace Lab4.ViewModels
{
    public class RsaViewModel : BaseViewModel
    {
        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
                RsaEncrypt.OnCanExecuteChanged();
                RsaDecrypt.OnCanExecuteChanged();
            }
        }

        public RsaOpenFile RsaOpenFile { get; set; } = new RsaOpenFile();

        public RsaDecrypt RsaDecrypt { get; set; } = new RsaDecrypt();

        public RsaEncrypt RsaEncrypt { get; set; } = new RsaEncrypt();

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
