using Euro2020BlazorApp.Models;
using Euro2020BlazorApp.Models.FootballData;
using System.Linq;

namespace Euro2020BlazorApp.Data
{
    public class TeamService
    {
        private readonly Models.FootballData.Team _teamFootballDataModel;

        public TeamService(Models.FootballData.Team teamFootballDataModel)
        {
            _teamFootballDataModel = teamFootballDataModel;
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

            return team;
        }

        private Player GetPlayerFromSquad(Squad squad)
        {
            return new Player()
            {
                PlayerID = squad.id,
                Name = squad.name,
                Position = squad.position,
                DateOfBirth = squad.dateOfBirth,
                CountryOfBirth = squad.countryOfBirth,
                Nationality = squad.nationality,
                ShirtNumber = squad.shirtNumber,
                Role = squad.role,
            };
        }
    }
}
