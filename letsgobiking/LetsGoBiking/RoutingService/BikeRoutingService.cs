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
        public async Task<List<JCDecauxStation>> GetStationsAsync()
        {
            var json = await _proxyService.GetStationsAsync();
            return JsonConvert.DeserializeObject<List<JCDecauxStation>>(json);
        }

        public async Task<JCDecauxStation> GetStationAsync(string id)
        {
            return JsonConvert.DeserializeObject<JCDecauxStation>(await _proxyService.GetStationAsync(id));
        }
    }
}
