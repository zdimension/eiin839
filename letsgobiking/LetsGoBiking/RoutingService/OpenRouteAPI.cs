using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using static Shared.Utilities;

namespace RoutingService
{
    internal class OpenRouteAPI
    {
        private static readonly HttpClient _client = new();
        private static readonly Lazy<string> _apiToken = new(() => Environment.GetEnvironmentVariable("OPENROUTE_API_KEY")
                                                   ?? throw new Exception("Missing API token (env var OPENROUTE_API_KEY)"));

        private static readonly Lazy<string> _apiRoot = new(() => $"https://api.openrouteservice.org/{{0}}?api_key={_apiToken.Value}&");

        private static async Task<HttpContent> PerformRequest(string endpoint, Dictionary<string, object>? args = null)
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

            Log($"Fetching {url}");
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return response.Content;
        }

        /// <summary>
        /// Sends a GET request to the specified endpoint asynchronously.
        /// </summary>
        /// <param name="endpoint">An API endpoint.</param>
        /// <param name="args">The parameters to the endpoint, as a dictionary.</param>
        /// <param name="cached">Whether to cache the request or not.</param>
        /// <returns>A task yielding the response as a stream.</returns>
        public static async Task<Stream> GetAsync(string endpoint, Dictionary<string, object>? args = null)
        {
            var content = await PerformRequest(endpoint, args);
            return await content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Sends a GET request to the specified endpoint asynchronously.
        /// </summary>
        /// <param name="endpoint">An API endpoint.</param>
        /// <param name="args">The parameters to the endpoint, as a dictionary.</param>
        /// <param name="cached">Whether to cache the request or not.</param>
        /// <returns>A task yielding the response as a raw string.</returns>
        public static async Task<string> GetAsyncString(string endpoint, Dictionary<string, object>? args = null)
        {
            var content = await PerformRequest(endpoint, args);
            return await content.ReadAsStringAsync();
        }
    }
}
