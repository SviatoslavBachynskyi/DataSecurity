namespace Lab2.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public TextHashingViewModel TextHashingViewModel { get; set; } = new TextHashingViewModel();

        public FileHashingViewModel FileHashingViewModel { get; set; } = new FileHashingViewModel();
    }
}
