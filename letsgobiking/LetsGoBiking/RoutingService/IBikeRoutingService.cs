using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace RoutingService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IRoutingService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IBikeRoutingService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetStations")]
        Task<List<JCDecauxStation>> GetStationsAsync();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetStation/{id}")]
        Task<JCDecauxStation> GetStationAsync(string id);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetRoute")]
        Task<Stream> GetRoute(RouteParameters points);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "Geocode")]
        Task<Stream> Geocode(GeocodeParameters geo);
    }

    [DataContract]
    public class RouteParameters
    {
        [DataMember] public JCDecauxPosition start { get; set; }
        [DataMember] public JCDecauxPosition end { get; set; }
    }

    [DataContract]
    public class GeocodeParameters
    {
        [DataMember] public string query { get; set; }
        [DataMember] public JCDecauxPosition focus { get; set; }
    }
}
