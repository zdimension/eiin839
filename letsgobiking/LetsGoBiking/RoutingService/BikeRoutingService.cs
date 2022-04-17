using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RoutingService.ProxyService;
using static Shared.Utilities;

namespace RoutingService
{
    public class BikeRoutingService : IBikeRoutingService
    {
        private static readonly ProxyServiceClient _proxyService = new();

        private static List<JCDecauxStation> _stations = null!;

        public BikeRoutingService()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        }

        public async Task<List<JCDecauxStation>> GetStationsAsync()
        {
            return _stations ??= JsonConvert.DeserializeObject<List<JCDecauxStation>>(await _proxyService.GetStationsAsync());
        }

        public async Task<JCDecauxStation> GetStationAsync(string id)
        {
            return JsonConvert.DeserializeObject<JCDecauxStation>(await _proxyService.GetStationAsync(id));
        }

        private async Task<JCDecauxStation> ClosestAvailable(JCDecauxPosition pos)
        {
            var stations = await GetStationsAsync();
            foreach (var s1 in stations.OrderBy(s => s.position.Distance(pos)))
            {
                if ((await GetStationAsync(s1.number.ToString())).totalStands.availabilities.bikes > 0) 
                    return s1;
                Log($"Station {s1.number} unavailable, trying next one");
            }
            Log("No stations found");
            return null;
        }

        private async Task<string> GetRouteFull(string type, JCDecauxPosition start, JCDecauxPosition end)
        {
            return await OpenRouteAPI.GetAsync($"v2/directions/{type}", new()
            {
                ["start"] = $"{start.longitude},{start.latitude}",
                ["end"] = $"{end.longitude},{end.latitude}"
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

        public async Task<string> Geocode(GeocodeParameters geo)
        {
            return await OpenRouteAPI.GetAsync("geocode/autocomplete", new()
            {
                ["text"] = geo.query,
                ["focus.point.lon"] = geo.focus.longitude,
                ["focus.point.lat"] = geo.focus.latitude,
                ["sources"] = "openstreetmap"
            });
        }
    }
}
