using FootballEngine.Services.Interfaces;
using FootballShared.Models;

namespace FootballEngine.Services;

public class PlayerSearchCacheService : IPlayerSearchCacheService
{
    private readonly PlayerSearchState _playerSearchState;
    private readonly FootballEngineInput _footballEngineInput;

    public PlayerSearchCacheService(PlayerSearchState playerSearchState, FootballEngineInput footballEngineInput)
    {
        _playerSearchState = playerSearchState;
        _footballEngineInput = footballEngineInput;
    }

    public void SavePlayerSearchToCache(PlayerSearchCriteria playerSearchCriteria)
    {
        _playerSearchState.PlayerSearchCriteria = playerSearchCriteria;
        _playerSearchState.LastRefreshTime = DateTime.UtcNow;
    }

    public PlayerSearchCriteria GetPlayerSearchFromCache()
    {
        if (_playerSearchState == null)
        {
            return null;
        }

        var playerSearchCriteria = new PlayerSearchCriteria();

        if (DateTime.UtcNow > _playerSearchState
        .LastRefreshTime
                                        .AddMinutes(_footballEngineInput.MinutesUntilRefreshPlayerSearchCache))
        {
            return playerSearchCriteria;
        }
        else
        {
            return _playerSearchState.PlayerSearchCriteria ?? playerSearchCriteria;
        }
    }
}
