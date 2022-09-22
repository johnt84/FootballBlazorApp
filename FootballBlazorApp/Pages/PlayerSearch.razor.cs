using FootballShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballBlazorApp.Pages
{
    public partial class PlayerSearch
    {
        private List<FootballShared.Models.Player> players = new List<FootballShared.Models.Player>();

        private PlayerSearchCriteria playerSearchCriteria = new PlayerSearchCriteria();

        protected override async Task OnInitializedAsync()
        {
            await SearchPlayersAsync();
        }

        private async Task ClearSearchAsync()
        {
            playerSearchCriteria = new PlayerSearchCriteria();
            await SearchPlayersAsync();
        }

        private async Task SearchPlayersAsync()
        {
            try
            {
                players = await FootballDataService.PlayerSearchAsync(playerSearchCriteria);
            }
            catch (Exception)
            {

            }
        }
    }
}
