using FootballEngine.Logic.Interfaces;
using FootballShared.Models;
using FootballShared.Models.FootballData;
using static FootballShared.Models.Enums.Enums;

namespace FootballEngine.Services
{
    public class TeamLogic : ITeamLogic
    {
        private readonly Teams _teamsFootballDataModel;
        private readonly FootballShared.Models.FootballData.Team _teamFootballDataModel;

        public TeamLogic(Teams teamsFootballDataModel)
        {
            _teamsFootballDataModel = teamsFootballDataModel;
        }

        public TeamLogic(FootballShared.Models.FootballData.Team teamFootballDataModel)
        {
            _teamFootballDataModel = teamFootballDataModel;
        }

        public List<FootballShared.Models.Team> GetTeams()
        {
            return _teamsFootballDataModel
                        .teams
                        .ToList()
                        .Select(x => new FootballShared.Models.Team()
                        {
                            TeamID = x.id,
                            Name = x.shortName,
                            TeamCrestUrl = x.crestUrl,
                        })
                        .OrderBy(x => x.Name)
                        .ToList();
        }

        public FootballShared.Models.Team GetTeam()
        {
            var team = new FootballShared.Models.Team()
            {
                TeamID = _teamFootballDataModel.id,
                Name = _teamFootballDataModel.shortName,
                TeamCrestUrl = _teamFootballDataModel.crestUrl,
                YearFounded = _teamFootballDataModel.founded,
                Website = _teamFootballDataModel.website,
                TeamColours = _teamFootballDataModel.clubColors,
                HomeStadium = _teamFootballDataModel.venue,
                Squad = _teamFootballDataModel
                            .squad
                            .ToList()
                            .Select(x => GetPlayerFromSquad(x, _teamFootballDataModel.id))
                            .ToList(),
            };

            team.CoachingStaff = GetCoachingStaff(team.Squad);
            team.SquadByPosition = GetPlayersByPosition(team.Squad);

            return team;
        }

        private List<Player> GetCoachingStaff(List<Player> squad)
        {
            var coachingStaff =  squad
                                .Where(x => x.SquadRole != SquadRole.Player)
                                .ToList();

            coachingStaff.ForEach(x => x.SquadSortOrder = GetSquadSortOrder(x.SquadRole.ToString()));

            return coachingStaff.OrderBy(x => x.SquadSortOrder).ToList();
        }

        private List<PlayerByPosition> GetPlayersByPosition(List<Player> squad)
        {
            return squad
                    .Where(x => x.SquadRole == SquadRole.Player && !string.IsNullOrEmpty(x.Position))
                    .GroupBy(x => x.Position)
                    .Select(x => new PlayerByPosition()
                    {
                        Position = x.Key,
                        SortOrder = GetSquadSortOrder(x.Key),
                        Players = x.ToList(),
                    })
                    .OrderBy(x => x.SortOrder)
                    .ToList();
        }

        private Player GetPlayerFromSquad(Squad squad, int teamID)
        {
            return new Player()
            {
                PlayerID = squad.id,
                SquadRole = GetSquadRole(squad.role),
                Name = squad.name,
                Position = squad.position,
                DateOfBirth = squad.dateOfBirth,
                CountryOfBirth = squad.countryOfBirth,
                Nationality = squad.nationality,
                ShirtNumber = squad.shirtNumber,
                Age = CalculateAge(squad.dateOfBirth),
                TeamID = teamID,
            };
        }

        private SquadRole GetSquadRole(string role)
        {
            var squadRole = SquadRole.Player;

            switch (role)
            {
                case "PLAYER":
                    squadRole = SquadRole.Player;
                    break;
                case "COACH":
                    squadRole = SquadRole.Coach;
                    break;
                case "ASSISTANT_COACH":
                    squadRole = SquadRole.Assistant_Coach;
                    break;
                default:
                    break;
            }

            return squadRole;
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            return dateOfBirth > today.AddYears(-age) 
                                ? age - 1 
                                : age;
        }

        private int GetSquadSortOrder(string position)
        {
            int squadSortOrder = (int)PlayerPosition.Goalkeeper;

            string positionForDisplay = position.Replace("_", " ");

            switch (positionForDisplay)
            {
                case "Goalkeeper":
                    squadSortOrder = (int)PlayerPosition.Goalkeeper;
                    break;
                case "Defender":
                    squadSortOrder = (int)PlayerPosition.Defender;
                    break;
                case "Midfielder":
                    squadSortOrder = (int)PlayerPosition.Midfielder;
                    break;
                case "Attacker":
                    squadSortOrder = (int)PlayerPosition.Attacker;
                    break;
                case "Coach":
                    squadSortOrder = (int)SquadRole.Coach;
                    break;
                case "Assistant Coach":
                    squadSortOrder = (int)SquadRole.Assistant_Coach;
                    break;
                default:
                    break;
            }

            return squadSortOrder;
        }
    }
}
