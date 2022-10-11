using FootballShared.Models;

namespace FootballEngine.Services.Interfaces
{
    public interface IPlayerSearchCacheService
    {
        void SavePlayerSearchToCache(PlayerSearchCriteria playerSearchCriteria);
        PlayerSearchCriteria GetPlayerSearchFromCache();
    }
}
