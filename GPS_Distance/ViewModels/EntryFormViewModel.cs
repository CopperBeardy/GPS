namespace GPS_Distance.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using CommonServiceLocator;
    using DistanceCalculator.Models;
    using GPS_Distance.Events;
    using GPS_Distance.Models;
    using Prism.Events;
    using static DistanceCalculator.Helpers.Helper;
    using static GPS_Distance.Helpers.Helper;

    //todo constrain the maximum size of the list box 
    //todo add scrollable to the list box control

    public class EntryFormViewModel : ValidationBaseViewModel
    {
        /*
         need to keep the Entry page from bring in the helper classes to reduce dependency 
         all processing for the results needs to be in the back end
         */
        #region Fields
        private ObservableCollection<Location> _endPointLocations = new ObservableCollection<Location>();
        private string _startLatitude = string.Empty;
        private string _startLongitude = string.Empty;
        private string _endLatitude = string.Empty;
        private string _endLongitude = string.Empty;
        private readonly IEventAggregator _eventAggregator;
        private Location _selectedItem = new Location();
        private string _notification = string.Empty;
        #endregion

        #region Properties
        public ObservableCollection<Location> EndPointsLocations
        {
            get => _endPointLocations;
            set => SetProperty(ref _endPointLocations, value);
        }
        public Location SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        public string Notification
        {
            get => _notification;
            set => SetProperty(ref _notification, value);
        }

        public string StartLatitude
        {
            get => _startLatitude;
            set
            {
                if (SetProperty(ref _startLatitude, value))
                    ValidateProperty(value, (v) => TryParseLatitude(v, out _), "Not a valid latitude");
            }
        }

        public string StartLongitude
        {
            get => _startLongitude;
            set
            {
                if (SetProperty(ref _startLongitude, value))
                    ValidateProperty(value, (v) => TryParseLongitude(v, out _), "Not a valid longitude");
            }
        }

        public string EndLatitude
        {
            get => _endLatitude;
            set
            {
                if (SetProperty(ref _endLatitude, value))
                    ValidateProperty(value, (v) => TryParseLatitude(v, out _), "Not a valid latitude");
            }
        }

        public string EndLongitude
        {
            get => _endLongitude;
            set
            {
                if (SetProperty(ref _endLongitude, value))
                    ValidateProperty(value, (v) => TryParseLongitude(v, out _), "Not a valid longitude");
            }
        }
        #endregion

        #region Commands
        public ICommand ClearStartValuesCommand { get; }
        public ICommand ClearEndValuesCommand { get; }
        public ICommand AddEndPointCommand { get; }
        public ICommand ClearEndPositionsListCommand { get; }
        public ICommand ResetFormCommand { get; }
        public ICommand MeasureDistanceCommand { get; }
        public ICommand ImportDataCommand { get; }
        public ICommand ExportDataCommand { get; }
        public ICommand RemoveEndPositionCommand { get; }

        #endregion

        #region Constructor
        public EntryFormViewModel()
        {
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            // Setup Command
            ClearStartValuesCommand = new RelayCommand(ClearStartValues);
            ClearEndValuesCommand = new RelayCommand(ClearEndValues);
            AddEndPointCommand = new RelayCommand(AddEndPoint);
            ClearEndPositionsListCommand = new RelayCommand(ClearEndPositionsList);
            ResetFormCommand = new RelayCommand(ResetForm);
            MeasureDistanceCommand = new RelayCommand(MeasureDistance);
            ImportDataCommand = new RelayCommand(ImportData);
            ExportDataCommand = new RelayCommand(ExportData);
            RemoveEndPositionCommand = new RelayCommand(RemoveEndPosition);
        }
        #endregion

        #region Methods
        /*
         Values are set to empty as the textbox has no Placeholder attribute
         if a value is put in it must be cleared before entering data this 
         is counter intuitive 
         */

        //TODO add ability to remove endPoint from list
        private void AddEndPoint()
        {
            if (!TryParseLatitude(EndLatitude, out var latitude)) return;
            if (!TryParseLongitude(EndLongitude, out var longitude)) return;

            EndPointsLocations.Add(new Location(latitude, longitude));
            ClearEndValues();
        }

        private void RemoveEndPosition()
        {
            if (SelectedItem != null)
            {
                EndPointsLocations.Remove(SelectedItem);
            }
        }

        private void MeasureDistance()
        {
            if (!TryParseLatitude(StartLatitude, out var latitude)) return;
            if (!TryParseLongitude(StartLongitude, out var longitude)) return;

            _eventAggregator.GetEvent<DistanceResultEvent>().Publish(
                new DistanceResultEventArgs
                {
                    InputDTO = new InputDTO
                    {
                        StartLocation = new Location(latitude, longitude),
                        EndLocations = EndPointsLocations
                    }
                });

            _eventAggregator.GetEvent<ResultTabEnablerEvent>().Publish(
                new ResultTabEnablerEventArgs { Enable = true });
        }

        private void ImportData() // Errors handled during import.
        {
            var fileName = ImportFromJson(out var startPoint, out var endPoints);

            if (fileName == string.Empty) Notification = "Import canceled by the user.";
            else if (fileName == "?") Notification = "Import failed to complete!";
            else if (startPoint is null) Notification = $"No End GPS Positions found in file '{fileName}', try another file.";
            else
            {
                Notification = $"Imported {endPoints.Count} End GPS Positions from file '{fileName}'.";

                StartLatitude = startPoint.Latitude.ToString(); // Updates screen.
                StartLongitude = startPoint.Longitude.ToString();

                foreach (var endPoint in endPoints)
                {
                    EndLatitude = endPoint.Latitude.ToString(); // Updates screen.
                    EndLongitude = endPoint.Longitude.ToString();
                    AddEndPoint();
                }
            }
        }

        private void ExportData() // Errors handled during export.
        {
            if (EndPointsLocations.Count == 0) Notification = "No End GPS Positions to Export.";
            else
            {
                var fileName = ExportToJson(StartLatitude, StartLongitude, EndPointsLocations);

                Notification = fileName == string.Empty ? "Export canceled by the user."
                             : fileName == "?" ? "Export failed to complete!"
                             : $"Exported {EndPointsLocations.Count} End GPS Positions to file '{fileName}'.";
            }
        }
        #endregion

        #region FormResetters
        private void ClearStartValues()
        {
            StartLatitude = string.Empty;
            StartLongitude = string.Empty;
        }

        private void ClearEndValues()
        {
            EndLatitude = string.Empty;
            EndLongitude = string.Empty;
        }

        private void ClearEndPositionsList()
        {
            EndPointsLocations.Clear();
        }

        private void ResetForm()
        {
            ClearStartValues();
            ClearEndValues();
            ClearEndPositionsList();
        }
        #endregion
    }
}
