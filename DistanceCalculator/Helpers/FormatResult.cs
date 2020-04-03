using System;
using System.Globalization;
using DistanceCalculator.Models;

namespace DistanceCalculator.Helpers
{
    public static partial class Helper
    {
        public static double FormatDouble(double result) => Math.Round(result, 4);
        public static double ToUnit(this double value, Unit unit, int digits = 4) => Math.Round(ConvertUnit(unit, value), digits);
        public static double ToDouble(this string value) => double.Parse(value.Replace(',', '.'), NumberFormatInfo.InvariantInfo);
    }
}
