namespace GPS_Distance.Events
{
    using GPS_Distance.Models;
    using Prism.Events;

    internal class ResultTabEnablerEvent : PubSubEvent<ResultTabEnablerEventArgs>
    { }

    public class ResultTabEnablerEventArgs
    {
        public bool Enable { get; set; }
    }
}
