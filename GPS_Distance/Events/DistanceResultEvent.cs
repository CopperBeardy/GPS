namespace GPS_Distance.Events
{
    using GPS_Distance.Models;
    using Prism.Events;

    internal class DistanceResultEvent : PubSubEvent<DistanceResultEventArgs>
    { }

    public class DistanceResultEventArgs
    {
        public InputDTO? InputDTO { get; set; }
    }
}
