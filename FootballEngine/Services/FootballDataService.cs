using FootballEngine.API.Interfaces;
using FootballEngine.Services.Interfaces;
using FootballShared.Models;
using FootballShared.Models.FootballData;
using System.Text.Json;

namespace FootballEngine.Services;

public class FootballDataService : IFootballDataService
{
    private readonly IHttpAPIClient _httpAPIClient;
    private readonly FootballDataState _footballDataState;
    private readonly FootballEngineInput _footballEngineInput;

    const string EurosCompetitionCode = "EC";

    private DateTime GetCompetitionStartDate(DateTime startDate) => !_footballDataState.CompetitionStartDate.HasValue
                                                                        ? startDate
                                                                        : _footballDataState.CompetitionStartDate.Value;

    private bool CacheRequiresRefresh(int hoursUntilRefreshCache) => DateTime.UtcNow > _footballDataState.CompetitionStartDate
                                                                    && DateTime.UtcNow > _footballDataState
                                                                                .LastRefreshTime
                                                                                .AddHours(hoursUntilRefreshCache);

    private bool IsTeamsCached(FootballDataState footballDataState) => footballDataState != null
                                                                && footballDataState.Teams != null;

    public FootballDataService(IHttpAPIClient httpAPIClient, FootballDataState footballDataState, FootballEngineInput footballEngineInput)
    {
        _httpAPIClient = httpAPIClient;
        _footballDataState = footballDataState;
        _footballEngineInput = footballEngineInput;
    }

    public async Task<List<GroupOrLeagueTableModel>> GetGroupsOrLeagueTableAsync()
    {
        var footballDataStandings = await GetFootballDataStandingsAsync();

        var groupOrLeagueTableLogic = new GroupOrLeagueTableLogic(footballDataStandings, _footballEngineInput);
        var groupsOrLeagueTable = groupOrLeagueTableLogic.GetGroupsOrLeagueTable();

        if (footballDataStandings.competition.code == EurosCompetitionCode)
        {
            groupOrLeagueTableLogic.BuildEurosThirdPlaceRankingTable(groupsOrLeagueTable);
        }

        return await GetFixturesAndResultsByGroupsAsync(groupsOrLeagueTable);
    }

    public async Task<List<FootballShared.Models.Team>> GetTeamsAsync()
    {
        if (IsTeamsCached(_footballDataState))
        {
            return _footballDataState.Teams;
        }

        return await GetTeamsAndUpdateCacheAsync();
    }

    public async Task<FootballShared.Models.Team> GetTeamAsync(int teamID)
    {
        List<FootballShared.Models.Team> teams = null;

        if (IsTeamsCached(_footballDataState))
        {
            teams = _footballDataState.Teams.ToList();
        }
        else
        {
            teams = await GetTeamsAndUpdateCacheAsync();
        }

        var team = teams
                    .Where(x => x.TeamID == teamID)
                    .FirstOrDefault();

        var footballDataMatches = await GetFootballDataMatchesAsync();

        var fixtureAndResultLogic = new FixtureAndResultLogic(footballDataMatches, _footballEngineInput);

        return fixtureAndResultLogic.GetFixturesAndResultsByTeam(team);
    }

    public async Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDaysAsync()
    {
        var footballDataMatches = await GetFootballDataMatchesAsync();

        var fixtureAndResultLogic = new FixtureAndResultLogic(footballDataMatches, _footballEngineInput);

        return fixtureAndResultLogic.GetFixturesAndResultsByDay();
    }

    public async Task<List<GroupOrLeagueTableModel>> GetFixturesAndResultsByGroupsAsync(List<GroupOrLeagueTableModel> groupsOrLeagueTable)
    {
        if (!groupsOrLeagueTable?.FirstOrDefault().IsGroup ?? false)
        {
            return groupsOrLeagueTable;
        }

        var footballDataMatches = await GetFootballDataMatchesAsync();

        var fixtureAndResultLogic = new FixtureAndResultLogic(footballDataMatches, _footballEngineInput);

        return fixtureAndResultLogic.GetFixturesAndResultsByGroupsOrLeagueTable(groupsOrLeagueTable);
    }

    public async Task<List<FootballShared.Models.Player>> PlayerSearchAsync(PlayerSearchCriteria playerSearchCriteria)
    {
        var players = await GetPlayersAsync();

        if (playerSearchCriteria != null)
        {
            players = players
                        .Where(x => ((playerSearchCriteria.PlayerAgeMinimum.HasValue && x.Age >= playerSearchCriteria.PlayerAgeMinimum.Value) || (!playerSearchCriteria.PlayerAgeMinimum.HasValue && x.Age == x.Age))
                            && ((playerSearchCriteria.PlayerAgeMaximum.HasValue && x.Age <= playerSearchCriteria.PlayerAgeMaximum.Value) || (!playerSearchCriteria.PlayerAgeMaximum.HasValue && x.Age == x.Age))
                            && ((!string.IsNullOrWhiteSpace(playerSearchCriteria.PlayerNationality) && !string.IsNullOrWhiteSpace(x.Nationality) && x.Nationality.ToLower().Contains(playerSearchCriteria.PlayerNationality.ToLower())) || (string.IsNullOrWhiteSpace(playerSearchCriteria.PlayerNationality) && x.Nationality == x.Nationality))
                            && ((playerSearchCriteria.PlayerPositions != null && playerSearchCriteria.PlayerPositions.Contains(x.Position) || (playerSearchCriteria.PlayerPositions == null ||  !playerSearchCriteria.PlayerPositions.Any()) && x.Position == x.Position))
                            && ((!string.IsNullOrWhiteSpace(playerSearchCriteria.PlayerName) && x.Name.ToLower().Contains(playerSearchCriteria.PlayerName.ToLower())) || (string.IsNullOrWhiteSpace(playerSearchCriteria.PlayerName) && x.Name == x.Name))
                            && ((!string.IsNullOrWhiteSpace(playerSearchCriteria.TeamName) && x.TeamName.ToLower().Contains(playerSearchCriteria.TeamName.ToLower())) || (string.IsNullOrWhiteSpace(playerSearchCriteria.TeamName) && x.TeamName == x.TeamName))
                            && ((playerSearchCriteria.TeamPositionMinimum.HasValue && x.TeamCurrentPosition >= playerSearchCriteria.TeamPositionMinimum.Value) || (!playerSearchCriteria.TeamPositionMinimum.HasValue && x.TeamCurrentPosition == x.TeamCurrentPosition))
                            && ((playerSearchCriteria.TeamPositionMaximum.HasValue && x.TeamCurrentPosition <= playerSearchCriteria.TeamPositionMaximum.Value) || (!playerSearchCriteria.TeamPositionMaximum.HasValue && x.TeamCurrentPosition == x.TeamCurrentPosition))
                            && ((playerSearchCriteria.PlayerConfederations != null && playerSearchCriteria.PlayerConfederations.Contains(x.ConfederationForDisplay.ToString()) || (playerSearchCriteria.PlayerConfederations == null || !playerSearchCriteria.PlayerConfederations.Any()) && x.ConfederationForDisplay == x.ConfederationForDisplay)))
                        .ToList();
        }

        return players;
    }

    public async Task<List<FootballShared.Models.Player>> GetPlayersAsync()
    {
        var groupsOrLeagueTable = await GetGroupsOrLeagueTableAsync();

        var teams = await GetTeamsAsync();

        var players = new List<FootballShared.Models.Player>();

        List<(string, int)> teamPositions = new List<(string, int)>();

        foreach (var team in teams)
        {
            foreach(var groupOrLeagueTable in groupsOrLeagueTable)
            {
                foreach(var groupOrLeagueTableStanding in groupOrLeagueTable.GroupOrLeagueTableStandings)
                {
                    if(team.Name == groupOrLeagueTableStanding.Team.Name)
                    {
                        teamPositions.Add((team.Name, groupOrLeagueTableStanding.Position));
                    }
                }
            }
        }

        foreach (var team in teams)
        {
            var teamPosition = teamPositions
                                .Where(x => x.Item1 == team.Name)
                                .FirstOrDefault();

            if (teamPosition.Item1 != null)
            {
                team.CurrentLeaguePosition = teamPosition.Item2;
            }
        }

        foreach (var team in teams)
        {
            foreach(var player in team.Squad)
            {
                player.TeamCurrentPosition = team.CurrentLeaguePosition;
            }

            players = players
                        .Union(team.Squad)
                        .ToList();
        }

        return players;
    }

    private async Task<FootballDataModel> GetFootballDataMatchesAsync()
    {
        bool footballDataMatchesCached = _footballDataState != null && _footballDataState.FootballDataMatches != null;
        bool footballDataMatchesCacheRequiresRefresh = !footballDataMatchesCached
                                            || CacheRequiresRefresh(_footballEngineInput.HoursUntilRefreshCache);

        FootballDataModel footballDataMatches = null;

        if (!footballDataMatchesCacheRequiresRefresh)
        {
            footballDataMatches = _footballDataState.FootballDataMatches;
        }
        else
        {
            footballDataMatches = await GetFootballDataMatchesFromAPIAsync();

            var startDate = footballDataMatches != null && footballDataMatches.matches != null
                            ? footballDataMatches.matches.ToList().First().season.startDate
                            : DateTime.MinValue;

            _footballDataState.FootballDataMatches = footballDataMatches;
            _footballDataState.CompetitionStartDate = GetCompetitionStartDate(startDate);
            MarkCacheAsRefreshed();
        }

        return footballDataMatches;
    }

    private async Task<FootballDataModel> GetFootballDataStandingsAsync()
    {
        bool footballDataStandingsCached = _footballDataState != null && _footballDataState.FootballDataStandings != null;
        bool footballDataModelCacheRequiresRefresh = !footballDataStandingsCached
                                            || CacheRequiresRefresh(_footballEngineInput.HoursUntilRefreshCache);

        FootballDataModel footballDataStandings = null;

        if (!footballDataModelCacheRequiresRefresh)
        {
            footballDataStandings = _footballDataState.FootballDataStandings;
        }
        else
        {
            footballDataStandings = await GetFootballDataStandingsFromAPIAsync();

            footballDataStandings.standings = footballDataStandings
                                                .standings
                                                .Where(x => x.type == "TOTAL")
                                                .ToArray();

            _footballDataState.FootballDataStandings = footballDataStandings;
            _footballDataState.CompetitionStartDate = GetCompetitionStartDate(footballDataStandings.season.startDate);
            MarkCacheAsRefreshed();
        }

        return footballDataStandings;
    }

    private async Task<List<FootballShared.Models.Team>> GetTeamsAndUpdateCacheAsync()
    {
        var footballDataTeams = await GetFootballDataTeamsFromAPIAsync();

        var teamLogic = new TeamLogic(footballDataTeams);

        var teams = teamLogic.GetTeams();

        _footballDataState.Teams = teams;
        _footballDataState.CompetitionStartDate = GetCompetitionStartDate(footballDataTeams.season.startDate);
        MarkCacheAsRefreshed();

        return teams;
    }

    private async Task<FootballDataModel> GetFootballDataMatchesFromAPIAsync()
    {
        string json = await _httpAPIClient.GetAsync($"competitions/{_footballEngineInput.Competition}/matches/");
        return JsonSerializer.Deserialize<FootballDataModel>(json);
    }

    private async Task<FootballDataModel> GetFootballDataStandingsFromAPIAsync()
    {
        string json = await _httpAPIClient.GetAsync($"competitions/{_footballEngineInput.Competition}/standings/");
        return JsonSerializer.Deserialize<FootballDataModel>(json);
    }

    private async Task<Teams> GetFootballDataTeamsFromAPIAsync()
    {
        string json = await _httpAPIClient.GetAsync($"competitions/{_footballEngineInput.Competition}/teams/");
        return JsonSerializer.Deserialize<Teams>(json);
    }
    
    private void MarkCacheAsRefreshed()
    {
        _footballDataState.LastRefreshTime = DateTime.UtcNow;
        _footballDataState.IsCacheRefreshed = true;
    }
}