namespace GPS_Distance.Models
{
    using System.Collections.ObjectModel;
    using DistanceCalculator.Models;

    /*
     entire purpose of this class is to transfer the input value for start and end locations
     from the entry for to what ever class is listening to the event handler
     */
    public class InputDTO
    {
        public Location? StartLocation { get; set; }
        public ObservableCollection<Location>? EndLocations { get; set; }
    }
}
