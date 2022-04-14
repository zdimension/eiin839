using System;
using System.Runtime.Serialization;

namespace RoutingService
{    
    [DataContract]
    public class JCDecauxStation
    {
        [DataMember] public int Number { get; set; }
        [DataMember] public string ContractName { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public string Address { get; set; }
        [DataMember] public JCDecauxPosition Position { get; set; }
        [DataMember] public bool Banking { get; set; }
        [DataMember] public bool Bonus { get; set; }
        [DataMember] public string Status { get; set; }
        [DataMember] public DateTime LastUpdate { get; set; }
        [DataMember] public bool Connected { get; set; }
        [DataMember] public bool Overflow { get; set; }
    }
}