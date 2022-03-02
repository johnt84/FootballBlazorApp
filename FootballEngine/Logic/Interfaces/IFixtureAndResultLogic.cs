using FootballShared.Models;

namespace FootballEngine.Logic.Interfaces
{
    public interface IFixtureAndResultLogic
    {
        Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDayAsync();
        Task<List<GroupOrLeagueTableModel>> GetFixturesAndResultsByGroupsOrLeagueTableAsync(List<GroupOrLeagueTableModel> groups);
        Task<FootballShared.Models.Team> GetFixturesAndResultsByTeamAsync(FootballShared.Models.Team team);
    }
}
