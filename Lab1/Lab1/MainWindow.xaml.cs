using System.Windows;
using Lab1.ViewModels;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel = new MainWindowViewModel()
            {
                InputParametersViewModel = new InputParametersViewModel()
                {
                    X0 = "8",
                    A = "1024",
                    C = "2",
                    M = "4095",
                    Count = "100"
                },
                OutputViewModel = new OutputViewModel()
                {
                    Period = ""
                }
            };

            this.DataContext = MainWindowViewModel;
        }
    }
}
