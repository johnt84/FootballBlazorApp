namespace FootballEngine.Logic.Interfaces
{
    public interface ITeamLogic
    {
        List<FootballShared.Models.Team> GetTeams();
        FootballShared.Models.Team GetTeam();
    }
}
