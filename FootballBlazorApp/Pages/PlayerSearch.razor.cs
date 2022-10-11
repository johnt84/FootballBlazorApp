using FootballShared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballBlazorApp.Pages
{
    public partial class PlayerSearch
    {
        private List<FootballShared.Models.Player> players = new List<FootballShared.Models.Player>();

        private PlayerSearchCriteria playerSearchCriteria = new PlayerSearchCriteria();

        protected override async Task OnInitializedAsync()
        {
            LoadCachedPlayerSearch();
            await SearchPlayersAsync();
        }

        private async Task ClearSearchAsync()
        {
            playerSearchCriteria = new PlayerSearchCriteria();
            await SearchPlayersAsync();
            PlayerSearchCacheService.SavePlayerSearchToCache(playerSearchCriteria);
        }

        private async Task SearchPlayersAsync()
        {
            try
            {
                players = await FootballDataService.PlayerSearchAsync(playerSearchCriteria);
                PlayerSearchCacheService.SavePlayerSearchToCache(playerSearchCriteria);
            }
            catch (Exception)
            {

            }
        }

        private void LoadCachedPlayerSearch()
        {
            playerSearchCriteria = PlayerSearchCacheService.GetPlayerSearchFromCache();
        }
    }
}
