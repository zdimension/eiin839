using System.Threading.Tasks;
using static Shared.Utilities;

namespace ProxyService;

public class ProxyService : IProxyService
{
    private const string ApiCity = "marseille";

    public async Task<string> GetStationsAsync()
    {
        Log("");
        return await JCDecauxAPI.GetAsync("stations", new() { ["contract"] = ApiCity }, false);
    }

    public async Task<string> GetStationAsync(string id)
    {
        Log(id);
        return await JCDecauxAPI.GetAsync("stations/" + id, new() { ["contract"] = ApiCity });
    }
}