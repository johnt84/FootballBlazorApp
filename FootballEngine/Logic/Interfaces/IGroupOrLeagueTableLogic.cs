using FootballShared.Models;

namespace FootballEngine.Logic.Interfaces
{
    public interface IGroupOrLeagueTableLogic
    {
        List<GroupOrLeagueTableModel> GetGroupsOrLeagueTable();
    }
}
