using FootballShared.Models;

namespace FootballEngine.Logic.Interfaces
{
    public interface IFixtureAndResultLogic
    {
        List<FixturesAndResultsByDay> GetFixturesAndResultsByDay();
        List<GroupOrLeagueTableModel> GetFixturesAndResultsByGroupsOrLeagueTable(List<GroupOrLeagueTableModel> groups);
        FootballShared.Models.Team GetFixturesAndResultsByTeam(Team team, FootballEngineInput footballEngineInput);
    }
}
