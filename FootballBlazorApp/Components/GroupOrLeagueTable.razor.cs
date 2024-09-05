using FootballShared.Models;
using FootballShared.Values;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;

namespace FootballBlazorApp.Components;

public partial class GroupOrLeagueTable
{
    [Parameter]
    public FootballShared.Models.GroupOrLeagueTableModel GroupOrLeagueTableModel { get; set; }

    [Inject]
    public IConfiguration Configuration { get; set; }

    private bool FixturesVisible { get; set; } = false;

    private bool IsSmall = false;

    private string hideOrShow => FixturesVisible ? "Hide" : "Show";

    private string TidyFormForDisplay(string form) => !string.IsNullOrWhiteSpace(form)
                                                        ? form.Replace(",", string.Empty)
                                                        : string.Empty;

    public int GetPosition(string groupName, GroupOrLeagueTableStanding standing) =>
        groupName == GroupNames.ThirdPlaceRankings && standing.ThirdRankingPosition is not null
                        ? standing.ThirdRankingPosition.Value
                        : standing.Position;

    private bool HasGroups => Convert.ToBoolean(Configuration["HasGroups"].ToString());

    private void ToggleVisbilityOfFixtures()
    {
        FixturesVisible = !FixturesVisible;
    }
}
