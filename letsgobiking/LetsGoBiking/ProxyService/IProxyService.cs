using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace ProxyService;

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