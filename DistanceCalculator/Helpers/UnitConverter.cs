using System;
using DistanceCalculator.Models;

namespace DistanceCalculator.Helpers
{
    public static partial class Helper
    {
        public static double DegreesToRadians(double degree) => degree * (Math.PI / 180);
        public static double RadiansToDegrees(double radian) => radian * (180 / Math.PI);

        public static double ConvertUnit(Unit unit, double distance) => unit switch
        {
            Unit.Metres => distance,
            Unit.Kilometres => distance / 1000,
            Unit.Miles => distance / 1609.344,
            _ => -1 // NOTE: Error, miles or runtime exception?
        };
    }
}
