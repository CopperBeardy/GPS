namespace GPS_Distance.Helpers
{
    using System.Collections.ObjectModel;
    using DistanceCalculator.Models;
    using GPS_Distance.Models;

    public static partial class Helper
    {
        public static ObservableCollection<DistanceResult> GenerateResults(MeasurementInputs measurementInputs, Unit selectedUnit)
        {
            var DistanceResults = new ObservableCollection<DistanceResult>();

            foreach (var route in measurementInputs.Routes)
            {
                DistanceResults.Add(new DistanceResult(measurementInputs.Start, route.End, selectedUnit));
            }

            return DistanceResults;
        }
    }
}
