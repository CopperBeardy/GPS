namespace DistanceCalculator.MeasurementFormulas
{
    using System;
    using DistanceCalculator.Models;

    public static partial class MeasureFormula
    {
        public static double HaversineMeasure(Route route) => HaversineMeasure(route.Start, route.End);
        public static double HaversineMeasure(Location startLocation, Location endLocation)
        {
            double dlat = endLocation.LatitudeRadians - startLocation.LatitudeRadians;
            double dlon = endLocation.LongitudeRadians - startLocation.LongitudeRadians;

            double squaredSinLat = Math.Sin(dlat / 2) * Math.Sin(dlat / 2);
            double squaredSinLon = Math.Sin(dlon / 2) * Math.Sin(dlon / 2);

            double squaredCos = Math.Cos(startLocation.LatitudeRadians) * Math.Cos(endLocation.LatitudeRadians);

            double squared = squaredSinLat + squaredSinLon * squaredCos;

            double distanceMetres = startLocation.EarthRadius * 2 * Math.Asin(Math.Sqrt(squared));

            return distanceMetres;
        }
    }
}

// Probably not gonna need two functions, backward compatibility for now.
