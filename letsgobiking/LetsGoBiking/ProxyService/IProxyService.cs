using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace ProxyService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IProxyService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IProxyService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetStations")]
        Task<string> GetStationsAsync();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetStation/{id}")]
        Task<string> GetStationAsync(string id);
    }
}
