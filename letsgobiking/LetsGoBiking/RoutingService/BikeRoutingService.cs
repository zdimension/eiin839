﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        private async Task<JCDecauxStation> ClosestAvailable(JCDecauxPosition pos, Func<JCDecauxStation, int> availGetter)
        {
            var stations = (await GetStationsAsync()).OrderBy(s => s.position.Distance(pos)).ToArray();
            var couples = (await Task.WhenAll(stations.Take(5)
                .Select(async station => (station, 
                    (await GetRouteWalking(pos, station.position))["features"][0]["properties"]["summary"]["distance"]))))
                .OrderBy(s => s.Item2.Value<double>())
                .Select(x => x.station);
            foreach (var s1 in couples.Concat(stations.Skip(5)))
            {
                if (availGetter(await GetStationAsync(s1.number.ToString())) > 0) 
                    return s1;
                Log($"Station {s1.number} unavailable, trying next one");
            }
            Log("No stations found");
            return null;
        }

        private async Task<JObject> GetRouteFull(string type, JCDecauxPosition start, JCDecauxPosition end)
        {
            return JsonConvert.DeserializeObject<JObject>(await OpenRouteAPI.GetAsyncString($"v2/directions/{type}", new()
            {
                ["start"] = $"{start.longitude},{start.latitude}",
                ["end"] = $"{end.longitude},{end.latitude}"
            }));
        }

        private async Task<JObject> GetRouteWalking(JCDecauxPosition start, JCDecauxPosition end)
        {
            return await GetRouteFull("foot-walking", start, end);
        }

        public async Task<Stream> GetRoute(RouteParameters points)
        {
            var closestStart = await ClosestAvailable(points.start, s => s.totalStands.availabilities.bikes);

            if (closestStart == null)
            {
                // no available stations
                return new[] { await GetRouteWalking(points.start, points.end) }.AsStream();
            }

            var closestEnd = await ClosestAvailable(points.end, s => s.totalStands.availabilities.stands);

            if (closestEnd == closestStart)
            {
                // only one available station
                return new[] { await GetRouteWalking(points.start, points.end) }.AsStream();
            }

            var routes = new[]
            {
                GetRouteWalking(points.start, closestStart.position),
                GetRouteFull("cycling-regular", closestStart.position, closestEnd.position),
                GetRouteWalking(closestEnd.position, points.end)
            };
            return (await Task.WhenAll(routes)).AsStream();
        }

        public async Task<Stream> Geocode(GeocodeParameters geo)
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
