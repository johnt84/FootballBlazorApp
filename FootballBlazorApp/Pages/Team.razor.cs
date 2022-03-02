using FootballShared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace FootballBlazorApp.Pages
{
    public partial class Team
    {
        private FootballShared.Models.Team team = null;

        private bool fixturesAndResultsExist => team?.FixturesAndResultsByDays != null
                                                && (team?.FixturesAndResultsByDays?.Count ?? 0) > 0;

        private bool FixturesVisible { get; set; } = false;

        private string hideOrShow => FixturesVisible ? "Hide" : "Show";

        private bool isInvalidTeam = false;

        private string ErrorMessage { get; set; }

        [Parameter]
        public int TeamID { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                team = await FootballDataService.GetTeamAsync(TeamID);
                isInvalidTeam = false;
            }
            catch (Exception ex) when (ex.Message.Contains(Constants.TOO_MANY_REQUESTS_ERROR_CODE.ToString()))
            {
                isInvalidTeam = true;
                ErrorMessage = Constants.TOO_MANY_REQUESTS_ERROR_MESSAGE;
            }
            catch (Exception)
            {
                isInvalidTeam = true;
                ErrorMessage = $"Could not retrieve a team with team id of {TeamID}";
            }
        }

        private void ToggleVisbilityOfFixtures()
        {
            FixturesVisible = !FixturesVisible;
        }
    }
}
