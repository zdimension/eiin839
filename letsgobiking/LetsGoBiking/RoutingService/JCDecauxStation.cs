using System;
using System.Runtime.Serialization;

namespace RoutingService;

[DataContract]
public class JCDecauxStation
{
    [DataMember] public int number { get; set; }
    [DataMember] public string contractName { get; set; }
    [DataMember] public string name { get; set; }
    [DataMember] public string address { get; set; }
    [DataMember] public JCDecauxPosition position { get; set; }
    [DataMember] public bool banking { get; set; }
    [DataMember] public bool bonus { get; set; }
    [DataMember] public string status { get; set; }
    [DataMember] public bool connected { get; set; }
    [DataMember] public JCDecauxStandInfo totalStands { get; set; }
}

[DataContract]
public class JCDecauxStandInfo
{
    [DataMember] public JCDecauxStandAvailability availabilities { get; set; }
    [DataMember] public int capacity { get; set; }
}

[DataContract]
public class JCDecauxStandAvailability
{
    [DataMember] public int bikes { get; set; }
    [DataMember] public int stands { get; set; }
}