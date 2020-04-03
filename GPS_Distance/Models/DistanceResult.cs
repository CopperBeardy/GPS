namespace GPS_Distance.Models
{
    using DistanceCalculator.Helpers;
    using DistanceCalculator.Models;

    public class DistanceResult : Route
    {
        public DistanceResult(Location start, Location end, Unit selectedUnit) : base(start, end)
        {
            ChangeUnit(selectedUnit);
        }

        // Properties
        public string EndLocation => End.LocationShort;
        public double ModifiedPythagorasResult { get; private set; }
        public double GreaterCircleResult { get; private set; }
        public double HaversineFormulaResult { get; private set; }
        public string StartName => Start.DisplayName; // Temp name.
        public string Message => End.DisplayName;     // Temp name.
        public string ShortName => End.ShortName;     // Temp name.

        //public string Message // Just for fun, but real logic and other properties may be handy.
        //{
        //    get // Don't do night flight navigation after these coordinates or tell your English teacher about the sentence structure.
        //    {
        //        var s = Start.Latitude >= 51.229 && Start.Latitude <= 53.635 && Start.Longitude >= -5.807 && Start.Longitude <= -2.649
        //            ? "You are probably in Wales. "
        //            : "Seems that you are not in Wales. ";

        //        s += End.Latitude >= 51.4 && End.Latitude <= 53.4 && End.Longitude >= -5.3 && End.Longitude <= -2.9
        //            ? "Are you going to Wales?"
        //            : "Are you leaving Wales?";

        //        return s;
        //    }
        //}

        // Methods
        public void ChangeUnit(Unit selectedUnit)
        {
            ModifiedPythagorasResult = ModifiedPythagoras.ToUnit(selectedUnit);
            GreaterCircleResult = GreaterCircle.ToUnit(selectedUnit);
            HaversineFormulaResult = Haversine.ToUnit(selectedUnit);
        }
    }
}
