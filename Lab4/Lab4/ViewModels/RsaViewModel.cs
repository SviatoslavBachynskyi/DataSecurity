using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Lab4.Commands;

namespace Lab4.ViewModels
{
    public class RsaViewModel : BaseViewModel
    {
        public const int EncryptBlockLength = 64;
        public const int DecryptBlockLength = 128;
        public RSACryptoServiceProvider CryptoService { get; } = new RSACryptoServiceProvider();

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

        public RsaImport RsaImport { get; set; } = new RsaImport();

        public RsaExport RsaExport { get; set; } = new RsaExport();

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
