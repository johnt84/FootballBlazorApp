using FootballEngine.API.Interfaces;
using FootballShared.Models;
using System.Net.Http.Headers;

namespace FootballEngine.API
{
    public class HttpAPIClient : IHttpAPIClient
    {
        public HttpClient _Client { get; }
        private readonly FootballEngineInput _footballEngineInput;

        public HttpAPIClient(HttpClient httpClient, FootballEngineInput footballEngineInput)
        {
            _footballEngineInput = footballEngineInput;
            _Client = httpClient;

            _Client.BaseAddress = new Uri(_footballEngineInput.FootballDataAPIUrl);
            _Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _Client.DefaultRequestHeaders.Add("X-Auth-Token", _footballEngineInput.APIToken);
        }

        public async Task<string> GetAsync(string url)
        {
            return await _Client.GetStringAsync(url);
        }
    }
}
