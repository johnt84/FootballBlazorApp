using FootballEngine.API.Interfaces;
using FootballEngine.Services.Interfaces;
using FootballShared.Models;
using FootballShared.Models.FootballData;
using System.Text.Json;

namespace FootballEngine.Services
{
    public class FootballDataService : IFootballDataService
    {
        private readonly IHttpAPIClient _httpAPIClient;
        private readonly FootballDataState _footballDataState;
        private readonly FootballEngineInput _footballEngineInput;

        private DateTime GetCompetitionStartDate(DateTime startDate) => !_footballDataState.CompetitionStartDate.HasValue
                                                                            ? startDate
                                                                            : _footballDataState.CompetitionStartDate.Value;

        private bool CacheRequiresRefresh(int hoursUntilRefreshCache) => DateTime.UtcNow > _footballDataState.CompetitionStartDate
                                                                        && DateTime.UtcNow > _footballDataState
                                                                                    .LastRefreshTime
                                                                                    .AddHours(hoursUntilRefreshCache);

        bool IsTeamsCached(FootballDataState footballDataState) => footballDataState != null
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

            var teamWithSquad = teams
                                .Where(x => x.TeamID == teamID
                                        && x.Squad != null && x.Squad.Count > 0)
                                .FirstOrDefault();

            var footballDataMatches = await GetFootballDataMatchesAsync();

            var fixtureAndResultLogic = new FixtureAndResultLogic(footballDataMatches, _footballEngineInput);

            return await fixtureAndResultLogic.GetFixturesAndResultsByTeamAsync(teamWithSquad);
        }

        public async Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDaysAsync()
        {
            var footballDataMatches = await GetFootballDataMatchesAsync();

            var fixtureAndResultLogic = new FixtureAndResultLogic(footballDataMatches, _footballEngineInput);

            return await fixtureAndResultLogic.GetFixturesAndResultsByDayAsync();
        }

        public async Task<List<GroupOrLeagueTableModel>> GetFixturesAndResultsByGroupsAsync(List<GroupOrLeagueTableModel> groupsOrLeagueTable)
        {
            var footballDataMatches = await GetFootballDataMatchesAsync();

            var fixtureAndResultLogic = new FixtureAndResultLogic(footballDataMatches, _footballEngineInput);

            return await fixtureAndResultLogic.GetFixturesAndResultsByGroupsOrLeagueTableAsync(groupsOrLeagueTable);
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
}