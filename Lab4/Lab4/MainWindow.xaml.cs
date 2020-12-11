using System.Windows;
using Lab4.ViewModels;

namespace Lab4
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

            MainWindowViewModel = new MainWindowViewModel();

            this.DataContext = MainWindowViewModel;
        }
    }
}