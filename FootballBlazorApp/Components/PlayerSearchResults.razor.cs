using FootballShared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace FootballBlazorApp.Components;

public partial class PlayerSearchResults
{
    [Parameter]
    public List<FootballShared.Models.Player> Players { get; set; }

    private bool IsSortedAscending;

    private string CurrentSortColumn;

    private bool IsSmall = false;

    private string GetPlayerDateOfBirth(Player player) => player.DateOfBirth.HasValue
                                                            ? player.DateOfBirth.Value.ToString("dd MMM yyyy")
                                                            : string.Empty;

    private string GetResultsTableClass(bool isSmall) => isSmall ? string.Empty : "playerSearchResultsTable";

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
            Players = Players.OrderBy(x =>
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
                Players = Players
                            .OrderByDescending(x =>
                                                  x.GetType()
                                                   .GetProperty(columnName)
                                                   .GetValue(x, null))
                             .ToList();
            }
            else
            {
                Players = Players
                                .OrderBy(x =>
                                        x.GetType()
                                         .GetProperty(columnName)
                                         .GetValue(x, null))
                                         .ToList();
            }

            IsSortedAscending = !IsSortedAscending;
        }
    }
}
