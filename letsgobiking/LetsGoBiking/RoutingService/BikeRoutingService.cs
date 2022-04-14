using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

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

        private async Task<JCDecauxStation> ClosestAvailable(JCDecauxPosition pos)
        {
            var stations = await _stations.Value;
            foreach (var s1 in stations.OrderBy(s => s.position.Distance(pos)))
            {
                if ((await GetStationAsync(s1.number.ToString())).totalStands.availabilities.bikes > 0) 
                    return s1;
            }

            return null;
        }

        private async Task<string> GetRouteFull(string type, JCDecauxPosition start, JCDecauxPosition end)
        {
            return await OpenRouteAPI.GetAsync($"v2/directions/{type}", new
            {
                start = $"{start.longitude},{start.latitude}",
                end = $"{end.longitude},{end.latitude}"
            });
        }

        private async Task<string> GetRouteWalking(JCDecauxPosition start, JCDecauxPosition end)
        {
            return await GetRouteFull("foot-walking", start, end);
        }

        public async Task<string[]> GetRoute(RouteParameters points)
        {
            var closestStart = await ClosestAvailable(points.start);

            if (closestStart == null)
            {
                // no available stations
                return new[] { await GetRouteWalking(points.start, points.end) };
            }

            var closestEnd = await ClosestAvailable(points.end);

            if (closestEnd == closestStart)
            {
                // only one available station
                return new[] { await GetRouteWalking(points.start, points.end) };
            }

            var routes = new[]
            {
                GetRouteWalking(points.start, closestStart.position),
                GetRouteFull("cycling-regular", closestStart.position, closestEnd.position),
                GetRouteWalking(closestEnd.position, points.end)
            };
            await Task.WhenAll(routes);
            return routes.Select(t => t.Result).ToArray();
        }

        public void CorsHack()
        {
        }

        public async Task<string> Geocode(GeocodeParameters geo)
        {
            return await OpenRouteAPI.GetAsync("geocode/autocomplete", (IDictionary<string, object>)new Dictionary<string, object>
            {
                ["text"] = geo.query,
                ["focus.point.lon"] = geo.focus.longitude,
                ["focus.point.lat"] = geo.focus.latitude,
                ["sources"] = "openstreetmap"
            });
        }

        public void CorsHack2()
        {
        }
    }
}
