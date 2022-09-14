using FootballShared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FootballBlazorApp.Pages
{
    public partial class PlayerProfile
    {
        [Parameter]
        public int PlayerID { get; set; }

        [Parameter]
        public int TeamID { get; set; }

        private FootballShared.Models.Team team = null;

        private Player player = null;

        private bool isInvalidTeam = false;

        private string ErrorMessage { get; set; }

        private bool isTeamOrPlayerNotSet => team == null || player == null;

        private bool isPageLoading => isTeamOrPlayerNotSet && !isInvalidTeam;

        private string playerDateOfBirth => player.DateOfBirth.HasValue
                                    ? player.DateOfBirth.Value.ToString("dd MMM yyyy")
                                    : string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                team = await FootballDataService.GetTeamAsync(TeamID);

                player = team
                            .Squad
                            .Where(x => x.PlayerID == PlayerID)
                            .FirstOrDefault();

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
