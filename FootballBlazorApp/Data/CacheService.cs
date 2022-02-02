using FootballBlazorApp.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace FootballBlazorApp.Data
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cache;
        private readonly IConfiguration _configuration;

        const string FOOTBALL_DATA_STATE = "footballdatastate";

        public CacheService(IConfiguration configuration)
        {
            _configuration = configuration;

            var connection = ConnectionMultiplexer.Connect(_configuration["RedisConfiguration"].ToString());
            _cache = connection.GetDatabase();
        }
        
        public async Task SaveToCacheAsync(FootballDataState footballDataState)
        {
            await _cache.StringSetAsync(FOOTBALL_DATA_STATE, JsonConvert.SerializeObject(footballDataState));
        }

        public async Task<FootballDataState> LoadFromCacheAsync()
        {
            string cachedFootballDatsState = await _cache.StringGetAsync(FOOTBALL_DATA_STATE);

            if (string.IsNullOrEmpty(cachedFootballDatsState))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<FootballDataState>(cachedFootballDatsState);
        }
    }
}
