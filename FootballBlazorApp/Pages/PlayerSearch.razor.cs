using FootballShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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

        private bool IsSortedAscending;

        private string CurrentSortColumn;

        private ElementReference playerPositionsList;

        private List<string> playerPositions = new List<string>()
        {
            "Goalkeeper",
            "Defender",
            "Midfielder",
            "Attacker",
        };

        private string GetPlayerDateOfBirth(Player player) => player.DateOfBirth.HasValue
                                                                ? player.DateOfBirth.Value.ToString("dd MMM yyyy")
                                                                : string.Empty;
        protected override async Task OnInitializedAsync()
        {
            await SearchPlayersAsync();
        }

        private async Task SearchAsync()
        {
            await SearchPlayersAsync();
        }

        private async Task ClearSearchAsync()
        {
            playerSearchCriteria = new PlayerSearchCriteria();
            await SearchPlayersAsync();
        }

        private async Task OnSelectionChanged(ChangeEventArgs eventArgs)
        {
            var selectedPositions = await GetSelectedItemsFromPlayerPositionsListAsync();
            playerSearchCriteria.PlayerPositions = selectedPositions;
        }

        private string GetSortStyle(string columnName)
        {
            if (CurrentSortColumn != columnName)
            {
                return string.Empty;
            }

            return IsSortedAscending ? "▲" : "▼";
        }

        private void SortTable(string columnName)
        {
            if (columnName != CurrentSortColumn)
            {
                players = players.OrderBy(x =>
                                        x.GetType()
                                        .GetProperty(columnName)
                                        .GetValue(x, null))
                                        .ToList();

                CurrentSortColumn = columnName;
                IsSortedAscending = true;

            }
            else
            {
                if (IsSortedAscending)
                {
                    players = players.OrderByDescending(x =>
                                                      x.GetType()
                                                       .GetProperty(columnName)
                                                       .GetValue(x, null))
                                 .ToList();
                }
                else
                {
                    players = players.OrderBy(x =>
                                            x.GetType()
                                             .GetProperty(columnName)
                                             .GetValue(x, null))
                                             .ToList();
                }

                IsSortedAscending = !IsSortedAscending;
            }
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

        public async Task<List<string>> GetSelectedItemsFromPlayerPositionsListAsync()
        {
            return (await _jsRuntime.InvokeAsync<List<string>>("getSelectedItemsInList", playerPositionsList)).ToList();
        }
    }
}
