using FootballShared.Models;

namespace FootballEngine.Services.Interfaces
{
    public interface IPlayerSearchService
    {
        void SavePlayerSearchToCache(PlayerSearchCriteria playerSearchCriteria);
        PlayerSearchCriteria GetPlayerSearchFromCache();
    }
}
