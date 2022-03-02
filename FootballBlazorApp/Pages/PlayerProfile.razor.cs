using FootballShared.Models;
using FootballShared.Models.Enums;
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

        private bool isPlayer => player.SquadRole == Enums.SquadRole.Player;

        private string positionOrRoleLabel => isPlayer ? "Position" : "Role";

        private string SquadRoleForDisplay(Enums.SquadRole squadRole) => squadRole.ToString().Replace("_", " ");

        private string positionOrRoleForDisplay => isPlayer
                                                    ? player.Position
                                                    : SquadRoleForDisplay(player.SquadRole);

        private bool isTeamOrPlayerNotSet => team == null || player == null;

        private bool isPageLoading => isTeamOrPlayerNotSet && !isInvalidTeam;

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
