using Euro2020BlazorApp.Models;
using Euro2020BlazorApp.Models.FootballData;
using System.Collections.Generic;
using System.Linq;
using static Euro2020BlazorApp.Models.Enums.Enums;

namespace Euro2020BlazorApp.Data
{
    public class TeamService
    {
        private readonly Teams _teamsFootballDataModel;
        private readonly Models.FootballData.Team _teamFootballDataModel;

        public TeamService(Models.FootballData.Teams teamsFootballDataModel)
        {
            _teamsFootballDataModel = teamsFootballDataModel;
        }

        public TeamService(Models.FootballData.Team teamFootballDataModel)
        {
            _teamFootballDataModel = teamFootballDataModel;
        }

        public List<Models.Team> GetTeams()
        {
            return _teamsFootballDataModel
                        .teams
                        .ToList()
                        .Select(x => new Models.Team()
                        {
                            TeamID = x.id,
                            Name = x.name,
                            TeamCrestUrl = x.crestUrl,
                        })
                        .OrderBy(x => x.Name)
                        .ToList();
        }

        public Models.Team GetTeam()
        {
            var team = new Models.Team()
            {
                TeamID = _teamFootballDataModel.id,
                Name = _teamFootballDataModel.name,
                TeamCrestUrl = _teamFootballDataModel.crestUrl,
                YearFounded = _teamFootballDataModel.founded,
                Website = _teamFootballDataModel.website,
                TeamColours = _teamFootballDataModel.clubColors,
                HomeStadium = _teamFootballDataModel.venue,
                Squad = _teamFootballDataModel
                            .squad
                            .ToList()
                            .Select(x => GetPlayerFromSquad(x))
                            .ToList(),
            };

            team.CoachingStaff = GetCoachingStaff(team.Squad);
            team.SquadByPosition = GetPlayersByPosition(team.Squad);

            return team;
        }

        private List<Player> GetCoachingStaff(List<Player> squad)
        {
            return squad
                    .Where(x => x.SquadRole != SquadRole.Player)
                    .ToList();
        }

        private List<PlayerByPosition> GetPlayersByPosition(List<Player> squad)
        {
            return squad
                    .Where(x => x.SquadRole == SquadRole.Player && !string.IsNullOrEmpty(x.Position))
                    .GroupBy(x => x.Position)
                    .Select(x => new PlayerByPosition()
                    {
                        Position = x.Key,
                        Players = x.ToList(),
                    })
                    .ToList();
        }

        private Player GetPlayerFromSquad(Squad squad)
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
    }
}
