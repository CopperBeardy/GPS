namespace DistanceCalculator.MeasurementFormulas
{
    using System;
    using DistanceCalculator.Models;

    public static partial class MeasureFormula
    {
        public static double ModifiedPythagorasMeasure(Route route) => ModifiedPythagorasMeasure(route.Start, route.End);
        public static double ModifiedPythagorasMeasure(Location startlocation, Location endLocation)
        {
            double lat = endLocation.Latitude - startlocation.Latitude;
            double lon = endLocation.Longitude - startlocation.Longitude;

            double squaredLat = Math.Pow(69.1 * lat, 2);
            double squaredLon = Math.Pow(53.0 * lon, 2);

            var distanceMetres = Math.Sqrt(squaredLat + squaredLon);

            return distanceMetres;
        }
    }
}

// Probably not gonna need two functions, backward compatibility for now.
