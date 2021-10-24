using FootballBlazorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballBlazorApp.Data
{
    interface IFootballDataService
    {
        public Task<List<GroupOrLeagueTableModel>> GetGroupsOrLeagueTable();
        public Task<List<Models.Team>> GetTeams();
        public Task<Models.Team> GetTeam(int teamID);
        public Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDays();
        public Task<List<GroupOrLeagueTableModel>> GetFixturesAndResultsByGroups(List<GroupOrLeagueTableModel> groups);
    }
}
