using Lab5.Commands;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Lab5.ViewModels
{
    public class TextViewModel : BaseViewModel
    {
        private SHA1 _sha1 = SHA1.Create();

        public MainWindowViewModel Main { get; set; }

        public TextViewModel(MainWindowViewModel main)
        {
            Main = main;
            Message = "";
        }

        private string _message;


        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                Main.Hash =
                    _sha1.ComputeHash(Encoding.Unicode.GetBytes(Message));
                OnPropertyChanged(Message);
                Generate.OnCanExecuteChanged();
            }
        }

        public GenerateMessageSignature Generate { get; set; } = new GenerateMessageSignature();
    }
}
