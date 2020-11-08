using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Lab2.Commands;

namespace Lab2.ViewModels
{
    public class FileHashingViewModel : BaseViewModel
    {
        private string _filePath;

        private string _generatedChecksum;

        private string _loadedChecksum;

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                GeneratedChecksum = String.Empty;
                LoadedChecksum = String.Empty;
                OnPropertyChanged(nameof(FilePath));
                Generate.OnCanExecuteChanged();
            }
        }

        public string GeneratedChecksum
        {
            get => _generatedChecksum;
            set
            {
                _generatedChecksum = value;
                OnPropertyChanged(nameof(GeneratedChecksum));
                SaveToFile.OnCanExecuteChanged();
                VerifyChecksum.OnCanExecuteChanged();
            }
        }

        public string LoadedChecksum
        {
            get => _loadedChecksum;
            set
            {
                _loadedChecksum = value;
                OnPropertyChanged(nameof(LoadedChecksum));
                OnPropertyChanged(nameof(VerifiedSuccessfully));
            }
        }

        public bool? VerifiedSuccessfully
        {
            get
            {
                if (String.IsNullOrEmpty(LoadedChecksum) || String.IsNullOrEmpty(GeneratedChecksum)) return null;

                return String.Equals(GeneratedChecksum, LoadedChecksum, StringComparison.CurrentCultureIgnoreCase);
            }
        }


        public OpenFile OpenFile { get; set; } = new OpenFile();

        public GenerateFileChecksum Generate { get; set; } = new GenerateFileChecksum();

        public SaveToFile SaveToFile { get; set; } = new SaveToFile();

        public VerifyChecksum VerifyChecksum { get; set; } = new VerifyChecksum();
    }
}
