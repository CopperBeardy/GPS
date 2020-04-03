namespace DistanceCalculator.Models
{
    using static DistanceCalculator.MeasurementFormulas.MeasureFormula;

    public class Route
    {
        public Route(Location start, Location end) { Start = start; End = end; }

        // Properties
        public Location Start { get; }
        public Location End { get; }
        public double GreaterCircle => GreaterCircleMeasure(this);
        public double Haversine => HaversineMeasure(this);
        public double ModifiedPythagoras => ModifiedPythagorasMeasure(this);

        #region Internal
        internal double DiffLatRad => End.LatitudeRadians - Start.LatitudeRadians;
        internal double DiffLonRad => End.LongitudeRadians - Start.LongitudeRadians;
        #endregion
    }
}
