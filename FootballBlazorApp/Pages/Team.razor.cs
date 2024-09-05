using FootballShared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace FootballBlazorApp.Pages;

public partial class Team
{
    private FootballShared.Models.Team team = null;

    private bool FixturesAndResultsExist => team?.FixturesAndResultsByDays != null
                                            && (team?.FixturesAndResultsByDays?.Count ?? 0) > 0;

    private bool FixturesVisible { get; set; } = false;

    private string HideOrShow => FixturesVisible ? "Hide" : "Show";

    private bool IsInvalidTeam = false;

    private string ErrorMessage { get; set; }

    private bool IsSmall = false;

    private string GetOverviewTableStyle(bool isSmall) => isSmall ? "width: 250px;" : "width: 630px;";

    [Parameter]
    public int TeamID { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            team = await FootballDataService.GetTeamAsync(TeamID);
            IsInvalidTeam = false;
        }
        catch (Exception ex) when (ex.Message.Contains(Constants.TOO_MANY_REQUESTS_ERROR_CODE.ToString()))
        {
            IsInvalidTeam = true;
            ErrorMessage = Constants.TOO_MANY_REQUESTS_ERROR_MESSAGE;
        }
        catch (Exception)
        {
            IsInvalidTeam = true;
            ErrorMessage = $"Could not retrieve a team with team id of {TeamID}";
        }
    }

    private void ToggleVisbilityOfFixtures()
    {
        FixturesVisible = !FixturesVisible;
    }
}
