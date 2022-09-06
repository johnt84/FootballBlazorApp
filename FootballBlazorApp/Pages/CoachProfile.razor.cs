using FootballShared.Models;
using FootballShared.Models.Enums;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace FootballBlazorApp.Pages
{
    public partial class CoachProfile
    {
        [Parameter]
        public int CoachID { get; set; }

        [Parameter]
        public int TeamID { get; set; }

        private FootballShared.Models.Team team = null;

        private Coach coach = null;

        private bool isInvalidTeam = false;

        private string ErrorMessage { get; set; }

        private string SquadRoleForDisplay(Enums.SquadRole squadRole) => squadRole.ToString().Replace("_", " ");

        private bool isTeamOrCoachNotSet => team == null || coach == null;

        private bool isPageLoading => isTeamOrCoachNotSet && !isInvalidTeam;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                team = await FootballDataService.GetTeamAsync(TeamID);

                coach = team.Coach;

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
    }
}