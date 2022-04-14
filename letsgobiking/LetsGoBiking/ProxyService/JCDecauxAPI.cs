using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace ProxyService
{
    internal class JCDecauxAPI
    {
        private static readonly HttpClient _client = new();
        private static readonly string _apiToken = Environment.GetEnvironmentVariable("JCDECAUX_API_KEY") 
                                                   ?? throw new Exception("Missing API token (env var JCDECAUX_API_KEY)");

        private static readonly string _apiRoot = $"https://api.jcdecaux.com/vls/v3/{{0}}?apiKey={_apiToken}";
        private static readonly ProxyCache<string> _cache = new(GetAsyncFresh);

        private static async Task<string> GetAsyncFresh(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public static async Task<string> GetAsync(string endpoint, Dictionary<string, string>? args = null)
        {
            var url = string.Format(_apiRoot, endpoint);
            
            if (args != null)
            {
                url = QueryHelpers.AddQueryString(url, args);
            }

            return await _cache.Get(url);
        }
    }
}
