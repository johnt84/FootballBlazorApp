using FootballShared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballBlazorApp.Pages;

public partial class Teams
{
    private List<FootballShared.Models.Team> TeamsList = null;

    private bool isInvalidTeams = false;

    private string ErrorMessage { get; set; }

    private bool IsSmall = false;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            TeamsList = await FootballDataService.GetTeamsAsync();
            isInvalidTeams = false;
        }
        catch (Exception ex) when (ex.Message.Contains(Constants.TOO_MANY_REQUESTS_ERROR_CODE.ToString()))
        {
            isInvalidTeams = true;
            ErrorMessage = Constants.TOO_MANY_REQUESTS_ERROR_MESSAGE;
        }
        catch (Exception)
        {
            isInvalidTeams = true;
            ErrorMessage = $"Could not retrieve any teams";
        }
    }
}
