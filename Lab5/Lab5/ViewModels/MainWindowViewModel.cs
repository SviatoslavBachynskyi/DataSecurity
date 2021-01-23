using System;
using System.Security.Cryptography;
using Lab5.Commands;

namespace Lab5.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public DSACryptoServiceProvider Dsa = new DSACryptoServiceProvider();

        public MainWindowViewModel()
        {
            FileViewModel = new FileViewModel(this);
            TextViewModel = new TextViewModel(this);
            SignatureControlViewModel = new SignatureControlViewModel(this);
        }

        public TextViewModel TextViewModel { get; set; }

        public FileViewModel FileViewModel { get; set; }
        public SignatureControlViewModel SignatureControlViewModel { get; set; }

        private byte[] _hash;

        public byte[] Hash
        {
            get => _hash;

            set
            {
                _hash = value;
                OnPropertyChanged(nameof(Hash));
                OnPropertyChanged(nameof(VerifiedSuccessfully));
            }
        }

        private string _signature;

        public string Signature
        {
            get => _signature;
            set
            {
                _signature = value;
                OnPropertyChanged(nameof(Signature));
                OnPropertyChanged(nameof(VerifiedSuccessfully));
                SignatureControlViewModel.SaveToFile.OnCanExecuteChanged();
            }
        }

        public bool? VerifiedSuccessfully
        {
            get
            {
                if (String.IsNullOrEmpty(Signature) || Hash == null || Hash.Length == 0)
                {
                    return null;
                }

                try
                {
                    return Dsa.VerifySignature(Hash, Convert.FromBase64String(Signature));
                }
                catch (FormatException)
                {
                    return false;
                }
                catch (CryptographicException)
                {
                    return false;
                }
            }
        }

        public Generate Generate { get; set; } = new Generate();
    }
}
