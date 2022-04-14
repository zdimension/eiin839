using System.Runtime.Serialization;

namespace RoutingService
{
    [DataContract]
    public class JCDecauxPosition
    {
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
    }
}
