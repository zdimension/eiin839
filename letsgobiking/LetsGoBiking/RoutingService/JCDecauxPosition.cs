using System;
using System.Runtime.Serialization;

namespace RoutingService;

[DataContract]
public class JCDecauxPosition
{
    [DataMember]
    public double latitude { get; set; }
    [DataMember]
    public double longitude { get; set; }

    public double Distance(JCDecauxPosition other)
    {
        const double earthRadius = 6371; // km
        var dLat = ToRadians(other.latitude - latitude);
        var dLon = ToRadians(other.longitude - longitude);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(latitude)) * Math.Cos(ToRadians(other.latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return earthRadius * c;
    }

    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}