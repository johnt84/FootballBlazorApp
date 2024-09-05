using FootballEngine.Logic.Interfaces;
using FootballShared.Models;
using FootballShared.Models.FootballData;
using static FootballShared.Models.Enums;

namespace FootballEngine.Services;

public class TeamLogic : ITeamLogic
{
    private readonly Teams _teamsFootballDataModel;
    private readonly FootballShared.Models.FootballData.Team _teamFootballDataModel;

    private List<CountriesInConfederation> countriesInConfederations => new List<CountriesInConfederation>()
    {
        new CountriesInConfederation()
        {
            Confederation = Confederation.Africa,
            ConfederationForDisplay = Confederation.Africa.ToString(),
            Countries = new List<string>()
            {
                "Algeria",
                "Burkina Faso",
                "Cape Verde",
                "Cameroon",
                "Central African Republic",
                "Comoros",
                "Cote d'Ivoire",
                "DR Congo",
                "Egypt",
                "Gabon",
                "Ghana",
                "Guinea",
                "Mali",
                "Martinique",
                "Mauritania",
                "Montserrat",
                "Morocco",
                "Nigeria",
                "Senegal",
                "South Africa",
                "Tunisia",
                "Zambia",
                "Zimbabwe",
            },
        },
        new CountriesInConfederation()
        {
            Confederation = Confederation.Asia,
            ConfederationForDisplay = Confederation.Asia.ToString(),
            Countries = new List<string>()
            {
                "China",
                "Iran",
                "Japan",
                "Korea, South",
                "Malaysia",
                "UAE",
            },
        },
        new CountriesInConfederation()
        {
            Confederation = Confederation.Carribean,
            ConfederationForDisplay = Confederation.Carribean.ToString(),
            Countries = new List<string>()
            {
                "Dominican Republic",
                "Grenada",
                "Jamaica",
                "Trinidad & Tobago",
            },
        },
        new CountriesInConfederation()
        {
            Confederation = Confederation.Europe,
            ConfederationForDisplay = Confederation.Europe.ToString(),
            Countries = new List<string>()
            {
                "Albania",
                "Armenia",
                "Azerbaijan",
                "Austria",
                "Belgium",
                "Bosnia and Herzegovina",
                "Bosnia-Herzegovina",
                "Bulgaria",
                "Croatia",
                "Czech Republic",
                "Denmark",
                "England",
                "Faroe Islands",
                "Finland",
                "France",
                "Georgia",
                "Germany",
                "Greece",
                "Hungary",
                "Iceland",
                "Ireland",
                "Israel",
                "Italy",
                "Kosovo",
                "Latvia",
                "Moldova",
                "Montenegro",
                "Netherlands",
                "North Macedonia",
                "Norway",
                "Northern Ireland",
                "Poland",
                "Portugal",
                "Republic of Ireland",
                "Romania",
                "Russia",
                "Scotland",
                "Serbia",
                "Slovakia",
                "Slovenia",
                "Spain",
                "Sweden",
                "Switzerland",
                "Turkey",
                "Ukraine",
                "Wales",
            },
        },
        new CountriesInConfederation()
        {
            Confederation = Confederation.NorthAndCentralAmerica,
            ConfederationForDisplay = "North & Central America",
            Countries = new List<string>()
            {
                "Canada",
                "Costa Rica",
                "Mexico",
                "United States",
            },
        },
        new CountriesInConfederation()
        {
            Confederation = Confederation.Oceania,
            ConfederationForDisplay = Confederation.Oceania.ToString(),
            Countries = new List<string>()
            {
                "Australia",
                "New Zealand"
            },
        },
        new CountriesInConfederation()
        {
            Confederation = Confederation.SouthAmerica,
            ConfederationForDisplay = "South America",
            Countries = new List<string>()
            {
                "Argentina",
                "Brazil",
                "Chile",
                "Colombia",
                "Ecuador",
                "Paraguay",
                "Peru",
                "Suriname",
                "Uruguay",
                "Venezuela",
            },
        },
    };

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
        var teams =  _teamsFootballDataModel
                    .teams
                    .ToList()
                    .Select(x => new FootballShared.Models.Team()
                    {
                        TeamID = x.id,
                        Name = x.shortName,
                        TeamCrestUrl = x.crest,
                        YearFounded = x.founded,
                        Website = x.website,
                        TeamColours = x.clubColors,
                        HomeStadium = x.venue,
                        Squad = x.squad
                                .ToList()
                                .Select(y => GetPlayerFromSquad(y, x.id, x.shortName))
                                .ToList(),
                        Coach = new FootballShared.Models.Coach() { 
                            CoachID = x.coach.id,
                            Name = x.coach.name,
                            DateOfBirth = x.coach.dateOfBirth,
                            Nationality = x.coach.nationality,
                            Age = CalculateAge(x.coach.dateOfBirth),
                            TeamID = x.id,
                            TeamName = x.shortName,
                            ConfederationForDisplay = GetPlayerConfederationForDisplay(x.coach.nationality),
                        },
                    })
                    .OrderBy(x => x.Name)
                    .ToList();

        teams.ForEach(x => x.SquadByPosition = GetPlayersByPosition(x.Squad));

        return teams;
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

    private Player GetPlayerFromSquad(Squad squad, int teamID, string teamName)
    {
        return new Player()
        {
            PlayerID = squad.id,
            Name = squad.name,
            Position = TidySquadPosition(squad.position),
            DateOfBirth = squad.dateOfBirth,
            Nationality = squad.nationality,
            Age = CalculateAge(squad.dateOfBirth),
            TeamID = teamID,
            TeamName = teamName,
            ConfederationForDisplay = GetPlayerConfederationForDisplay(squad.nationality),
        };
    }

    private int CalculateAge(DateTime? dateOfBirth)
    {
        if (!dateOfBirth.HasValue)
        {
            return 0;
        }
        
        var today = DateTime.Today;
        int age = today.Year - dateOfBirth.Value.Year;
        return dateOfBirth > today.AddYears(-age) 
                            ? age - 1 
                            : age;
    }

    private string TidySquadPosition(string position)
    {
        if(string.IsNullOrWhiteSpace(position))
        {
            return string.Empty;
        }
        
        string tidySquadPosition = string.Empty;

        string positionForDisplay = position.Replace("_", " ");

        switch (positionForDisplay)
        {
            case "Goalkeeper":
                tidySquadPosition = PlayerPosition.Goalkeeper.ToString();
                break;
            case "Defence":
            case "Defender":
            case "Centre-Back":
            case "Left-Back":
            case "Right-Back":
                tidySquadPosition = PlayerPosition.Defender.ToString();
                break;
            case "Midfield":
            case "Midfielder":
            case "Defensive Midfield":
            case "Attacking Midfield":
            case "Central Midfield":
            case "Left Midfield":
            case "Right Midfield":
            case "Left Winger":
            case "Right Winger":
                tidySquadPosition = PlayerPosition.Midfielder.ToString();
                break;
            case "Offence":
            case "Centre-Forward":
            case "Forward":
                tidySquadPosition = PlayerPosition.Attacker.ToString();
                break;
            default:
                break;
        }

        return tidySquadPosition;
    }

    private int GetSquadSortOrder(string position)
    {
        int squadSortOrder = (int)PlayerPosition.Goalkeeper;

        switch (position)
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
            default:
                break;
        }

        return squadSortOrder;
    }

    private string GetPlayerConfederationForDisplay(string nationality)
    {
        if(string.IsNullOrWhiteSpace(nationality))
        {
            return "Unknown";
        }
        
        var countriesInConfederation = countriesInConfederations
                                        .Where(x => x.Countries.Contains(nationality))
                                        .FirstOrDefault();

        return countriesInConfederation?.ConfederationForDisplay ?? "Unknown";
    }
}
