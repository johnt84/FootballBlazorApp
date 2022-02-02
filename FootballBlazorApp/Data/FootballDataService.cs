using FootballBlazorApp.API;
using FootballBlazorApp.Models;
using FootballBlazorApp.Models.FootballData;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FootballBlazorApp.Data
{
    public class FootballDataService : IFootballDataService
    {
        private readonly IHttpAPIClient _httpAPIClient;
        private readonly ITimeZoneOffsetService _timeZoneOffsetService;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;

        private DateTime GetCompetitionStartDate(FootballDataState footballDataState, DateTime startDate) => !footballDataState.CompetitionStartDate.HasValue
                                                                            ? startDate
                                                                            : footballDataState.CompetitionStartDate.Value;

        private bool CacheRequiresRefresh(FootballDataState footballDataState) => DateTime.UtcNow > footballDataState.CompetitionStartDate
                                                                                    && DateTime.UtcNow > footballDataState
                                                                                                .LastRefreshTime
                                                                                                .AddHours(Convert.ToInt32(_configuration["HoursUntilRefreshCache"].ToString()));

        bool IsTeamsCached(FootballDataState footballDataState) => footballDataState != null 
                                                                    && footballDataState.Teams != null;

        public FootballDataService(IHttpAPIClient httpAPIClient, ITimeZoneOffsetService timeZoneOffsetService
                                    , IConfiguration configuration, ICacheService cacheService)
        {
            _httpAPIClient = httpAPIClient;
            _timeZoneOffsetService = timeZoneOffsetService;
            _configuration = configuration;
            _cacheService = cacheService;
        }

        public async Task<List<GroupOrLeagueTableModel>> GetGroupsOrLeagueTable()
        {
            var footballDataStandings = await GetFootballDataStandings();

            var groupOrLeagueTableService = new GroupOrLeagueTableService(footballDataStandings, _configuration);
            var groups = groupOrLeagueTableService.GetGroupsOrLeagueTable();

            return await GetFixturesAndResultsByGroups(groups);
        }

        public async Task<List<Models.Team>> GetTeams()
        {
            var footballDataState = await _cacheService.LoadFromCacheAsync();

            if (IsTeamsCached(footballDataState))
            {
                return footballDataState.Teams;
            }

            return await GetTeamsAndUpdateCache();
        }

        public async Task<Models.Team> GetTeam(int teamID)
        {
            List<Models.Team> teams = null;

            var footballDataState = await _cacheService.LoadFromCacheAsync();

            if (IsTeamsCached(footballDataState))
            {
                teams = footballDataState.Teams.ToList();
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

                var teamInCacheToUpdate = footballDataState
                                            .Teams
                                            .First(x => x.TeamID == teamID);

                int teamInCacheToUpdateIndex = footballDataState.Teams.IndexOf(teamInCacheToUpdate);
                footballDataState.Teams[teamInCacheToUpdateIndex] = teamWithSquad;

                await _cacheService.SaveToCacheAsync(footballDataState);
            }

            var footballDataMatches = await GetFootballDataMatches();

            var fixtureAndResultService = new FixtureAndResultService(footballDataMatches, _timeZoneOffsetService);
            return await fixtureAndResultService.GetFixturesAndResultsByTeam(teamWithSquad);
        }

        public async Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDays()
        {
            var footballDataMatches = await GetFootballDataMatches();

            var fixtureAndResultService = new FixtureAndResultService(footballDataMatches, _timeZoneOffsetService);

            return await fixtureAndResultService.GetFixturesAndResultsByDay();
        }

        public async Task<List<GroupOrLeagueTableModel>> GetFixturesAndResultsByGroups(List<GroupOrLeagueTableModel> groupsOrLeagueTable)
        {
            var footballDataMatches = await GetFootballDataMatches();

            var fixtureAndResultService = new FixtureAndResultService(footballDataMatches, _timeZoneOffsetService);

            return await fixtureAndResultService.GetFixturesAndResultsByGroupsOrLeagueTable(groupsOrLeagueTable);
        }

        private async Task<FootballDataModel> GetFootballDataMatches()
        {
            var footballDataState = await _cacheService.LoadFromCacheAsync();
            
            bool footballDataMatchesCached = footballDataState != null && footballDataState.FootballDataMatches != null;
            bool footballDataMatchesCacheRequiresRefresh = !footballDataMatchesCached
                                                || CacheRequiresRefresh(footballDataState);

            FootballDataModel footballDataMatches = null;

            if (!footballDataMatchesCacheRequiresRefresh)
            {
                footballDataMatches = footballDataState.FootballDataMatches;
            }
            else
            {
                footballDataMatches = await GetFootballDataMatchesFromAPI();

                var startDate = footballDataMatches != null && footballDataMatches.matches != null
                                ? footballDataMatches.matches.ToList().First().season.startDate
                                : DateTime.MinValue;

                if (footballDataState == null)
                {
                    footballDataState = new FootballDataState();
                }

                footballDataState.FootballDataMatches = footballDataMatches;
                footballDataState.CompetitionStartDate = GetCompetitionStartDate(footballDataState, startDate);
                footballDataState.LastRefreshTime = DateTime.UtcNow;

                await _cacheService.SaveToCacheAsync(footballDataState);
            }

            return footballDataMatches;
        }

        private async Task<FootballDataModel> GetFootballDataStandings()
        {
            var footballDataState = await _cacheService.LoadFromCacheAsync();

            bool footballDataStandingsCached = footballDataState != null && footballDataState.FootballDataStandings != null;
            bool footballDataModelCacheRequiresRefresh = !footballDataStandingsCached
                                                || CacheRequiresRefresh(footballDataState);

            FootballDataModel footballDataStandings = null;

            if (!footballDataModelCacheRequiresRefresh)
            {
                footballDataStandings = footballDataState.FootballDataStandings;
            }
            else
            {
                footballDataStandings = await GetFootballDataStandingsFromAPI();

                if (footballDataState == null)
                {
                    footballDataState = new FootballDataState();
                }

                footballDataState.FootballDataStandings = footballDataStandings;
                footballDataState.CompetitionStartDate = GetCompetitionStartDate(footballDataState, footballDataStandings.season.startDate);
                footballDataState.LastRefreshTime = DateTime.UtcNow;

                await _cacheService.SaveToCacheAsync(footballDataState);
            }

            return footballDataStandings;
        }

        private async Task<List<Models.Team>> GetTeamsAndUpdateCache()
        {
            var footballDataTeams = await GetFootballDataTeamsFromAPI();

            var teamService = new TeamService(footballDataTeams);

            var teams = teamService.GetTeams();

            var footballDataState = new FootballDataState()
            {
                Teams = teams,

                LastRefreshTime = DateTime.UtcNow
            };

            footballDataState.CompetitionStartDate = GetCompetitionStartDate(footballDataState, footballDataTeams.season.startDate);

            await _cacheService.SaveToCacheAsync(footballDataState);

            return teams;
        }

        private async Task<FootballDataModel> GetFootballDataMatchesFromAPI()
        {
            string json = await _httpAPIClient.Get($"competitions/{_configuration["Competition"]}/matches/");
            return JsonSerializer.Deserialize<FootballDataModel>(json);
        }

        private async Task<FootballDataModel> GetFootballDataStandingsFromAPI()
        {
            string json = await _httpAPIClient.Get($"competitions/{_configuration["Competition"]}/standings/");
            return JsonSerializer.Deserialize<FootballDataModel>(json);
        }

        private async Task<Teams> GetFootballDataTeamsFromAPI()
        {
            string json = await _httpAPIClient.Get($"competitions/{_configuration["Competition"]}/teams/");
            return JsonSerializer.Deserialize<Teams>(json);
        }

        private async Task<Models.FootballData.Team> GetFootballDataTeamFromAPI(int teamID)
        {
            string json = await _httpAPIClient.Get($"teams/{teamID}");
            return JsonSerializer.Deserialize<Models.FootballData.Team>(json);
        }
    }
}
