using System;

namespace DistanceCalculator.Helpers
{
    public static partial class Helper
    {
        private const double EquatorialEarthRadius = 6378137;
        private const double PoleEarthRadius = 6371001;

        public static double RadiusLatitudeAdjustment(double Latitude)
        {
            var value1 = Math.Pow(Math.Pow(EquatorialEarthRadius, 2) * Math.Cos(Latitude), 2);
            var value2 = Math.Pow(Math.Pow(PoleEarthRadius, 2) * Math.Sin(Latitude), 2);

            var value3 = Math.Pow(EquatorialEarthRadius * Math.Cos(Latitude), 2);
            var value4 = Math.Pow(PoleEarthRadius * Math.Sin(Latitude), 2);

            return Math.Sqrt((value1 + value2) / (value3 + value4));
        }
    }
}
