using Lab4.Commands;

namespace Lab4.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public Rc5ViewModel Rc5ViewModel { get; set; }  = new Rc5ViewModel();

        public RsaViewModel RsaViewModel { get; set; } = new RsaViewModel();
    }
}
