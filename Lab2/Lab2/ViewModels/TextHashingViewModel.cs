using System;
using System.Windows.Input;
using Lab2.Commands;

namespace Lab2.ViewModels
{
    public class TextHashingViewModel : BaseViewModel
    {
        private string _message;

        private string _checksum;

        public string Message {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                Checksum = String.Empty;
            }
        }

        public string Checksum
        {
            get => _checksum;
            set
            {
                _checksum = value;
                OnPropertyChanged(nameof(Checksum));
                SaveToFile.OnCanExecuteChanged();
            }
        }

        public GenerateMessageChecksum Generate { get; set; } = new GenerateMessageChecksum();

        public SaveTextChecksumToFile SaveToFile { get; set; } = new SaveTextChecksumToFile();
    }
}
