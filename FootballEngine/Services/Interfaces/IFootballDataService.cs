using FootballShared.Models;

namespace FootballEngine.Services.Interfaces
{
    public interface IFootballDataService
    {
        public Task<List<GroupOrLeagueTableModel>> GetGroupsOrLeagueTable();
        public Task<List<FootballShared.Models.Team>> GetTeams();
        public Task<FootballShared.Models.Team> GetTeam(int teamID);
        public Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDays();
        public Task<List<GroupOrLeagueTableModel>> GetFixturesAndResultsByGroups(List<GroupOrLeagueTableModel> groups);
    }
}
