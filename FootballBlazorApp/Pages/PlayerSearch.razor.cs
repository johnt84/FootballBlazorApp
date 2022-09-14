using FootballShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FootballShared.Models.Enums;

namespace FootballBlazorApp.Pages
{
    public partial class PlayerSearch
    {
        private List<FootballShared.Models.Player> players = new List<FootballShared.Models.Player>();

        private PlayerSearchCriteria playerSearchCriteria = new PlayerSearchCriteria();

        private bool IsSortedAscending;

        private string CurrentSortColumn;

        private ElementReference playerPositionsList;

        private ElementReference playerConfederationsList;

        private List<string> playerPositions = new List<string>()
        {
            PlayerPosition.Goalkeeper.ToString(),
            PlayerPosition.Defender.ToString(),
            PlayerPosition.Midfielder.ToString(),
            PlayerPosition.Attacker.ToString(),
        };

        private List<string> playerConfederations = new List<string>()
        {
            Confederation.Africa.ToString(),
            Confederation.Asia.ToString(),
            Confederation.Carribean.ToString(),
            Confederation.Europe.ToString(),
            "North & Central America",
            Confederation.Oceania.ToString(),
            "South America",
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

        private async Task OnPlayerPositionsListSelectionChanged(ChangeEventArgs eventArgs)
        {
            var selectedPositions = await GetSelectedItemsFromPlayerPositionsListAsync();
            playerSearchCriteria.PlayerPositions = selectedPositions;
        }

        private async Task OnPlayerConfederationsListSelectionChanged(ChangeEventArgs eventArgs)
        {
            var selectedConfederations = await GetSelectedItemsFromPlayerConfederationsListAsync();
            playerSearchCriteria.PlayerConfederations = selectedConfederations;
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
            return await GetSelectedItemsFromListAsync(playerPositionsList);
        }

        public async Task<List<string>> GetSelectedItemsFromPlayerConfederationsListAsync()
        {
            return await GetSelectedItemsFromListAsync(playerConfederationsList);
        }

        private async Task<List<string>> GetSelectedItemsFromListAsync(ElementReference dropdownList)
        {
            return (await _jsRuntime.InvokeAsync<List<string>>("getSelectedItemsInList", dropdownList)).ToList();
        }
    }
}
