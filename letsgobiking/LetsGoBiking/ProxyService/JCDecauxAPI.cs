using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using static Shared.Logging;

namespace ProxyService
{
    internal class JCDecauxAPI
    {
        private static readonly HttpClient _client = new();
        private static readonly Lazy<string> _apiToken = new(() => Environment.GetEnvironmentVariable("JCDECAUX_API_KEY") 
                                                   ?? throw new Exception("Missing API token (env var JCDECAUX_API_KEY)"));

        private static readonly Lazy<string> _apiRoot = new(() => $"https://api.jcdecaux.com/vls/v3/{{0}}?apiKey={_apiToken.Value}&");
        private static readonly ProxyCache<string> _cache = new(GetAsyncFresh);

        /// <summary>
        /// Sends a GET request to the specified URL asynchronously.
        /// </summary>
        /// <param name="url">A URL.</param>
        /// <returns>A task yielding the response as a raw string.</returns>
        private static async Task<string> GetAsyncFresh(string url)
        {
            Log($"Fetching {url}");
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        /// <summary>
        /// Sends a GET request to the specified endpoint asynchronously.
        /// </summary>
        /// <param name="endpoint">An API endpoint.</param>
        /// <param name="args">The parameters to the endpoint, as a dictionary.</param>
        /// <param name="cached">Whether to cache the request or not.</param>
        /// <returns>A task yielding the response as a raw string.</returns>
        public static async Task<string> GetAsync(string endpoint, Dictionary<string, object>? args = null, bool cached = true)
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

            return await (cached ? _cache.Get(url) : GetAsyncFresh(url));
        }
    }
}
