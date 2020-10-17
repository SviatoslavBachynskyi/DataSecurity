namespace Lab1.ViewModels
{
    public class OutputViewModel : BaseViewModel
    {
        private string _period;

        private string _generatedSequence;

        public string Period
        {
            get => _period;
            set
            {
                _period = value;
                OnPropertyChanged(nameof(Period));
            }
        }


        public string GeneratedSequence
        {
            get => _generatedSequence;
            set
            {
                _generatedSequence = value;
                OnPropertyChanged(nameof(GeneratedSequence));
            }
        }
    }
}
