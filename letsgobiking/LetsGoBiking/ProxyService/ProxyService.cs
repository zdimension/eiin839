using System.Threading.Tasks;
using System.Web.Routing;

namespace ProxyService
{
    public class ProxyService : IProxyService
    {
        private static readonly string _apiCity = "lyon";

        public async Task<string> GetStationsAsync()
        {
            return await JCDecauxAPI.GetAsync("stations", new { contract = _apiCity }, false);
        }

        public async Task<string> GetStationAsync(string id)
        {
            return await JCDecauxAPI.GetAsync("stations/" + id, new { contract = _apiCity });
        }
    }
}
