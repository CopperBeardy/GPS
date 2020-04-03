namespace DistanceCalculator.MeasurementFormulas
{
    using System;
    using DistanceCalculator.Models;

    public static partial class MeasureFormula
    {
        public static double GreaterCircleMeasure(Route route) => GreaterCircleMeasure(route.Start, route.End);
        public static double GreaterCircleMeasure(Location startLocation, Location endLocation)
        {
            double sineAngle = Math.Sin(startLocation.LatitudeRadians) * Math.Sin(endLocation.LatitudeRadians);
            double cosAngle = Math.Cos(startLocation.LatitudeRadians) * Math.Cos(endLocation.LatitudeRadians)
                            * Math.Cos(endLocation.LongitudeRadians - startLocation.LongitudeRadians);

            double distanceMetres = startLocation.EarthRadius * Math.Acos(sineAngle + cosAngle);

            return distanceMetres;
        }
    }
}

// Probably not gonna need two functions, backward compatibility for now.
