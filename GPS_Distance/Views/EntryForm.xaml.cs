namespace GPS_Distance.Views
{
    using System.Windows.Controls;
    using GPS_Distance.ViewModels;

    /// <summary>
    /// Interaction logic for EntryForm.xaml
    /// </summary>
    public partial class EntryForm : UserControl
    {
        public EntryForm()
        {
            InitializeComponent();
            DataContext = new EntryFormViewModel();
        }
    }
}
