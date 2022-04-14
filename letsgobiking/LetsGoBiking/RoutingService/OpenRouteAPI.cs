using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace RoutingService
{
    internal class OpenRouteAPI
    {
        private static readonly HttpClient _client = new();
        private static readonly Lazy<string> _apiToken = new(() => Environment.GetEnvironmentVariable("OPENROUTE_API_KEY")
                                                   ?? throw new Exception("Missing API token (env var OPENROUTE_API_KEY)"));

        private static readonly Lazy<string> _apiRoot = new(() => $"https://api.openrouteservice.org/v2/{{0}}?api_key={_apiToken.Value}&");

        /// <summary>
        /// Sends a GET request to the specified endpoint asynchronously.
        /// </summary>
        /// <param name="endpoint">An API endpoint.</param>
        /// <param name="args">The parameters to the endpoint, as a dictionary.</param>
        /// <param name="cached">Whether to cache the request or not.</param>
        /// <returns>A task yielding the response as a raw string.</returns>
        public static async Task<string> GetAsync(string endpoint, IDictionary<string, object>? args = null)
        {
            var url = string.Format(_apiRoot.Value, endpoint);

            if (args != null)
            {
                var query = HttpUtility.ParseQueryString("");
                foreach (var entry in args)
                {
                    query.Add(entry.Key, entry.Value.ToString());
                }

                url += query.ToString();
            }

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        /// <summary>
        /// Sends a GET request to the specified endpoint asynchronously.
        /// </summary>
        /// <param name="endpoint">An API endpoint.</param>
        /// <param name="args">The parameters to the endpoint, as an anonymous type.</param>
        /// <param name="cached">Whether to cache the request or not.</param>
        /// <returns>A task yielding the response as a raw string.</returns>
        public static async Task<string> GetAsync<T>(string endpoint, T args)
        {
            return await GetAsync(endpoint, (IDictionary<string, object>)new RouteValueDictionary(args));
        }
    }
}
