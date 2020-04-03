namespace DistanceCalculator.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class MeasurementInputs
    {
        public MeasurementInputs(Location start) : this(start.Latitude, start.Longitude) { }
        public MeasurementInputs(double latitude, double longitude)
        {
            Start = new Location(latitude, longitude);
            Routes = new List<Route>();
        }

        // Properties
        public Location Start { get; }
        public List<Route> Routes { get; }

        // Methods
        public void AddEndPoint(double latitude, double longitude)
        {
            Routes.Add(new Route(Start, new Location(latitude, longitude)));
        }
        public void AddEndPoints(Collection<Location> locations)
        {
            foreach (var loc in locations) AddEndPoint(loc.Latitude, loc.Longitude);
        }
    }
}
