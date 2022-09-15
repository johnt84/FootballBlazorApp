using FootballShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballBlazorApp.Pages
{
    public partial class GroupsOrLeagueTable
    {
        private List<FootballShared.Models.GroupOrLeagueTableModel> groupsOrLeagueTable = null;

        private bool isInvalidGroupsOrLeagueTable = false;

        private string groupEmblemForDisplay => groupsOrLeagueTable?.FirstOrDefault().Emblem ?? string.Empty;

        private string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                groupsOrLeagueTable = await FootballDataService.GetGroupsOrLeagueTableAsync();
                isInvalidGroupsOrLeagueTable = false;
            }
            catch (Exception ex) when (ex.Message.Contains(Constants.TOO_MANY_REQUESTS_ERROR_CODE.ToString()))
            {
                isInvalidGroupsOrLeagueTable = true;
                ErrorMessage = Constants.TOO_MANY_REQUESTS_ERROR_MESSAGE;
            }
            catch (Exception)
            {
                isInvalidGroupsOrLeagueTable = true;
                ErrorMessage = $"Could not retrieve any groups or league table";
            }
        }
    }
}
