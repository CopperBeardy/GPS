namespace GPS_Distance.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using CommonServiceLocator;
    using DistanceCalculator.Models;
    using GPS_Distance.Events;
    using GPS_Distance.Models;
    using Prism.Events;
    using static GPS_Distance.Helpers.Helper;

    public class DistanceResultsViewModel : BaseViewModel
    {
        #region Fields
        private MeasurementInputs? _measurementInputs;
        private List<Location> _endLocations = new List<Location>();
        private ObservableCollection<DistanceResult> _distanceResults = new ObservableCollection<DistanceResult>();
        private Unit _selectedUnit = Unit.Metres;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<Unit> _units = new ObservableCollection<Unit>();
        #endregion

        #region Properties
        public MeasurementInputs? MeasurementInputs
        {
            get => _measurementInputs ?? null;
            set => SetProperty(ref _measurementInputs, value);
        }

        public List<Location> EndPositions
        {
            get => _endLocations;
            set => SetProperty(ref _endLocations, value);
        }

        public ObservableCollection<DistanceResult> DistanceResults
        {
            get => _distanceResults;
            set => SetProperty(ref _distanceResults, value);
        }
        public ObservableCollection<Unit> Units
        {
            get => _units;
            set => SetProperty(ref _units, value);
        }

        public Unit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                if (SetProperty(ref _selectedUnit, value))
                    GenerateSourceData();
            }
        }
        #endregion

        #region Commands
        public ICommand GenerateSourceDataCommand { get; }

        #endregion

        #region Constructor
        public DistanceResultsViewModel()
        {
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _eventAggregator.GetEvent<DistanceResultEvent>().Subscribe(DistanceResultEventHandler);
            GenerateSourceDataCommand = new RelayCommand(GenerateSourceData);

            foreach (Unit unit in (Unit[])Enum.GetValues(typeof(Unit)))
            {
                Units.Add(unit);
            }
        }

        private void DistanceResultEventHandler(DistanceResultEventArgs obj)
        {
            if (obj.InputDTO is null) return;

            MeasurementInputs = new MeasurementInputs(obj.InputDTO.StartLocation);
            MeasurementInputs.AddEndPoints(obj.InputDTO.EndLocations);
            GenerateSourceData();
        }
        #endregion

        #region Methods
        private void GenerateSourceData()
        {
            if (MeasurementInputs is null) return;

            DistanceResults = GenerateResults(MeasurementInputs, SelectedUnit);
        }
        #endregion
    }
}
