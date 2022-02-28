using FootballEngine.API;
using FootballEngine.Services;
using FootballEngine.Services.Interfaces;
using FootballShared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FootballEngineUnitTest
{
    [TestClass]
    public class FootballServiceTest
    {
        [TestMethod]
        public async Task GetGroupsOrLeagueTableTest()
        {
            var httpClient = new HttpClient();

            var footballEngineInput = new FootballEngineInput()
            {
                FootballDataAPIUrl = "https://api.football-data.org/v2/",
                Competition = "PL",
                HasGroups = false,
                LeagueName = "Premier League",
                APIToken = "",
                HoursUntilRefreshCache = 3,
            };

            httpClient.BaseAddress = new Uri(footballEngineInput.FootballDataAPIUrl);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", footballEngineInput.APIToken);

            var httpAPIClient = new HttpAPIClient(httpClient, footballEngineInput);

            var footballDataService = new FootballDataService(httpAPIClient, new FootballDataState(), footballEngineInput);

            var groupsOrLeagueTable = await footballDataService.GetGroupsOrLeagueTable();
        }
    }
}
