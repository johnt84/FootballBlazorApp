using FootballEngine.API;
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

        private DateTime GetCompetitionStateDate(DateTime startDate) => !_footballDataState.CompetitionStartDate.HasValue
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

        public async Task<List<GroupOrLeagueTableModel>> GetGroupsOrLeagueTable()
        {
            var footballDataStandings = await GetFootballDataStandings();

            var groupOrLeagueTableService = new GroupOrLeagueTableService(footballDataStandings, _footballEngineInput);
            var groups = groupOrLeagueTableService.GetGroupsOrLeagueTable();

            return await GetFixturesAndResultsByGroups(groups);
        }

        public async Task<List<FootballShared.Models.Team>> GetTeams()
        {
            if (IsTeamsCached(_footballDataState))
            {
                return _footballDataState.Teams;
            }

            return await GetTeamsAndUpdateCache();
        }

        public async Task<FootballShared.Models.Team> GetTeam(int teamID)
        {
            List<FootballShared.Models.Team> teams = null;

            if (IsTeamsCached(_footballDataState))
            {
                teams = _footballDataState.Teams.ToList();
            }
            else
            {
                teams = await GetTeamsAndUpdateCache();
            }

            var teamWithSquad = teams
                                .Where(x => x.TeamID == teamID
                                        && x.Squad != null && x.Squad.Count > 0)
                                .FirstOrDefault();

            if (teamWithSquad == null)
            {
                var footballDataTeam = await GetFootballDataTeamFromAPI(teamID);

                var teamService = new TeamService(footballDataTeam);
                teamWithSquad = teamService.GetTeam();

                var teamInCacheToUpdate = _footballDataState
                                            .Teams
                                            .First(x => x.TeamID == teamID);

                int teamInCacheToUpdateIndex = _footballDataState.Teams.IndexOf(teamInCacheToUpdate);
                _footballDataState.Teams[teamInCacheToUpdateIndex] = teamWithSquad;
            }

            var footballDataMatches = await GetFootballDataMatches();

            var fixtureAndResultService = new FixtureAndResultService(footballDataMatches, _footballEngineInput);
            return await fixtureAndResultService.GetFixturesAndResultsByTeam(teamWithSquad);
        }

        public async Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDays()
        {
            var footballDataMatches = await GetFootballDataMatches();

            var fixtureAndResultService = new FixtureAndResultService(footballDataMatches, _footballEngineInput);

            return await fixtureAndResultService.GetFixturesAndResultsByDay();
        }

        public async Task<List<GroupOrLeagueTableModel>> GetFixturesAndResultsByGroups(List<GroupOrLeagueTableModel> groupsOrLeagueTable)
        {
            var footballDataMatches = await GetFootballDataMatches();

            var fixtureAndResultService = new FixtureAndResultService(footballDataMatches, _footballEngineInput);

            return await fixtureAndResultService.GetFixturesAndResultsByGroupsOrLeagueTable(groupsOrLeagueTable);
        }

        private async Task<FootballDataModel> GetFootballDataMatches()
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
                footballDataMatches = await GetFootballDataMatchesFromAPI();

                var startDate = footballDataMatches != null && footballDataMatches.matches != null
                                ? footballDataMatches.matches.ToList().First().season.startDate
                                : DateTime.MinValue;

                _footballDataState.FootballDataMatches = footballDataMatches;
                _footballDataState.CompetitionStartDate = GetCompetitionStateDate(startDate);
                _footballDataState.LastRefreshTime = DateTime.UtcNow;
            }

            return footballDataMatches;
        }

        private async Task<FootballDataModel> GetFootballDataStandings()
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
                footballDataStandings = await GetFootballDataStandingsFromAPI();
                _footballDataState.FootballDataStandings = footballDataStandings;
                _footballDataState.CompetitionStartDate = GetCompetitionStateDate(footballDataStandings.season.startDate);
                _footballDataState.LastRefreshTime = DateTime.UtcNow;
            }

            return footballDataStandings;
        }

        private async Task<List<FootballShared.Models.Team>> GetTeamsAndUpdateCache()
        {
            var footballDataTeams = await GetFootballDataTeamsFromAPI();

            var teamService = new TeamService(footballDataTeams);

            var teams = teamService.GetTeams();

            _footballDataState.Teams = teams;
            _footballDataState.CompetitionStartDate = GetCompetitionStateDate(footballDataTeams.season.startDate);
            _footballDataState.LastRefreshTime = DateTime.UtcNow;

            return teams;
        }

        private async Task<FootballDataModel> GetFootballDataMatchesFromAPI()
        {
            string json = await _httpAPIClient.Get($"competitions/{_footballEngineInput.Competition}/matches/");
            return JsonSerializer.Deserialize<FootballDataModel>(json);
        }

        private async Task<FootballDataModel> GetFootballDataStandingsFromAPI()
        {
            string json = await _httpAPIClient.Get($"competitions/{_footballEngineInput.Competition}/standings/");
            return JsonSerializer.Deserialize<FootballDataModel>(json);
        }

        private async Task<Teams> GetFootballDataTeamsFromAPI()
        {
            string json = await _httpAPIClient.Get($"competitions/{_footballEngineInput.Competition}/teams/");
            return JsonSerializer.Deserialize<Teams>(json);
        }

        private async Task<FootballShared.Models.FootballData.Team> GetFootballDataTeamFromAPI(int teamID)
        {
            string json = await _httpAPIClient.Get($"teams/{teamID}");
            return JsonSerializer.Deserialize<FootballShared.Models.FootballData.Team>(json);
        }
    }
}