namespace GPS_Distance.Views
{
    using System.Windows.Controls;
    using GPS_Distance.ViewModels;

    /// <summary>
    /// Interaction logic for DistanceResults.xaml
    /// </summary>
    public partial class DistanceResults : UserControl
    {
        public DistanceResults()
        {
            InitializeComponent();
            DataContext = new DistanceResultsViewModel();          
        }
    }
}
