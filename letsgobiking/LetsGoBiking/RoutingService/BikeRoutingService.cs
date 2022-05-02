using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoutingService.ProxyService;
using static Shared.Utilities;

namespace RoutingService;

public class BikeRoutingService : IBikeRoutingService
{
    private static readonly ProxyServiceClient ProxyService = new();

    private static readonly Lazy<Task<List<JCDecauxStation>>> Stations = new(async () =>
        JsonConvert.DeserializeObject<List<JCDecauxStation>>(await ProxyService.GetStationsAsync()));

    public BikeRoutingService()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
    }

    public async Task<List<JCDecauxStation>> GetStationsAsync()
    {
        return await Stations.Value;
    }

    public async Task<JCDecauxStation> GetStationAsync(string id)
    {
        return JsonConvert.DeserializeObject<JCDecauxStation>(await ProxyService.GetStationAsync(id));
    }

    private async Task<JCDecauxStation> ClosestAvailable(JCDecauxPosition pos, Func<JCDecauxStation, int> availGetter)
    {
        // closest by raw distance
        var stations = (await GetStationsAsync()).OrderBy(s => s.position.Distance(pos)).ToArray();
        // compute walking distance only for closest five
        var stationsDistances = stations.Take(5)
            .Select(async station => (station,
                (await GetRouteWalking(pos, station.position))["features"]![0]!["properties"]!["summary"]!["distance"]));
        // closest by walking distance
        var closest = (await Task.WhenAll(stationsDistances))
            .OrderBy(s => s.Item2!.Value<double>())
            .Select(x => x.station);
        foreach (var s1 in closest.Concat(stations.Skip(5)))
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

    private async Task<JObject> GetRouteCycling(JCDecauxPosition start, JCDecauxPosition end)
    {
        return await GetRouteFull("cycling-regular", start, end);
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
            GetRouteCycling(closestStart.position, closestEnd.position),
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