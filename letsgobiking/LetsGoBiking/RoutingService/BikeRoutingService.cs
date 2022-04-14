using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RoutingService.ProxyService;

namespace RoutingService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "RoutingService" à la fois dans le code et le fichier de configuration.
    public class BikeRoutingService : IBikeRoutingService
    {
        private readonly ProxyServiceClient _proxyService = new();

        private readonly Lazy<Task<List<JCDecauxStation>>> _stations;

        public BikeRoutingService()
        {
            _stations = new(async () =>
            {
                var json = await _proxyService.GetStationsAsync();
                return JsonConvert.DeserializeObject<List<JCDecauxStation>>(json);
            });
        }

        public async Task<List<JCDecauxStation>> GetStationsAsync()
        {
            return await _stations.Value;
        }

        public async Task<JCDecauxStation> GetStationAsync(string id)
        {
            return JsonConvert.DeserializeObject<JCDecauxStation>(await _proxyService.GetStationAsync(id));
        }

        public async Task<string> GetRoute(string start, string end)
        {
            return await OpenRouteAPI.GetAsync("directions/cycling-regular", new
            {
                start,
                end
            });
        }
    }
}
