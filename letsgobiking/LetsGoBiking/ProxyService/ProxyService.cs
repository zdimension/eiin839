using System.Threading.Tasks;
using static Shared.Utilities;

namespace ProxyService
{
    public class ProxyService : IProxyService
    {
        private static readonly string _apiCity = "marseille";

        public async Task<string> GetStationsAsync()
        {
            Log("");
            return await JCDecauxAPI.GetAsync("stations", new() { ["contract"] = _apiCity }, false);
        }

        public async Task<string> GetStationAsync(string id)
        {
            Log(id);
            return await JCDecauxAPI.GetAsync("stations/" + id, new() { ["contract"] = _apiCity });
        }
    }
}
