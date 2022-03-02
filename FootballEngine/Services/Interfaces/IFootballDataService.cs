using FootballShared.Models;

namespace FootballEngine.Services.Interfaces
{
    public interface IFootballDataService
    {
        public Task<List<GroupOrLeagueTableModel>> GetGroupsOrLeagueTableAsync();
        public Task<List<FootballShared.Models.Team>> GetTeamsAsync();
        public Task<FootballShared.Models.Team> GetTeamAsync(int teamID);
        public Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDaysAsync();
        public Task<List<GroupOrLeagueTableModel>> GetFixturesAndResultsByGroupsAsync(List<GroupOrLeagueTableModel> groups);
    }
}
