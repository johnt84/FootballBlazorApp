using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FootballBlazorApp.API
{
    public class HttpAPIClient : IHttpAPIClient
    {
        public HttpClient _Client { get; }
        private readonly IConfiguration _configuration;

        public HttpAPIClient(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _Client = httpClient;

            _Client.BaseAddress = new Uri(_configuration["FootballDataAPIUrl"]);
            _Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _Client.DefaultRequestHeaders.Add("X-Auth-Token", _configuration["APIToken"]);
        }

        public async Task<string> Get(string url)
        {
            return await _Client.GetStringAsync(url);
        }
    }
}
