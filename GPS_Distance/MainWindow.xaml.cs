using GPS_Distance.ViewModels;
using System.Windows;

namespace GPS_Distance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();

        }

       

    }
}