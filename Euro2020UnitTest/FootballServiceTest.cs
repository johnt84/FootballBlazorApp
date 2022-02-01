using FootballBlazorApp.API;
using FootballBlazorApp.Data;
using FootballBlazorApp.Models;
using FootballBlazorApp.Models.FootballData;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FootballBlazorAppUnitTest
{
    [TestClass]
    public class FootballServiceTest
    {
        private readonly Mock<ITimeZoneOffsetService> _mockTimeZoneOffsetService = new Mock<ITimeZoneOffsetService>();
        private readonly Mock<FootballDataState> _mockFootballDataState = new Mock<FootballDataState>();

        [TestMethod]
        public async Task GetGroupsTest()
        {
            var configuration = GetTestConfiguration();

            string url = "testURL";

            string testJson = @"{
                               ""filters"":{

                                        },
                               ""competition"":{
                                            ""id"":2021,
                                  ""area"":{
                                                ""id"":2072,
                                     ""name"":""England""
                                  },
                                  ""name"":""Premier League"",
                                  ""code"":""PL"",
                                  ""plan"":""TIER_ONE"",
                                  ""lastUpdated"":""2021 - 04 - 17T02: 20:14Z""
                               },
                               ""season"":{
                                            ""id"":733,
                                  ""startDate"":""2021 - 08 - 13"",
                                  ""endDate"":""2022 - 05 - 22"",
                                  ""currentMatchday"":23,
                                  ""winner"":null
                               },
                               ""standings"":[
                                  {
                                            ""stage"":""REGULAR_SEASON"",
                                     ""type"":""TOTAL"",
                                     ""group"":null,
                                     ""table"":[
                                        {
                                                ""position"":1,
                                           ""team"":{
                                                    ""id"":65,
                                              ""name"":""Manchester City FC"",
                                              ""crestUrl"":""https://crests.football-data.org/65.svg""
                                           },
                                           ""playedGames"":23,
                                           ""form"":null,
                                           ""won"":18,
                                           ""draw"":3,
                                           ""lost"":2,
                                           ""points"":57,
                                           ""goalsFor"":55,
                                           ""goalsAgainst"":14,
                                           ""goalDifference"":41
                                        },
                                        {
                                                ""position"":2,
                                           ""team"":{
                                                    ""id"":64,
                                              ""name"":""Liverpool FC"",
                                              ""crestUrl"":""https://crests.football-data.org/64.png""
                                           },
                                           ""playedGames"":22,
                                           ""form"":null,
                                           ""won"":14,
                                           ""draw"":6,
                                           ""lost"":2,
                                           ""points"":48,
                                           ""goalsFor"":58,
                                           ""goalsAgainst"":19,
                                           ""goalDifference"":39
                                        },
                                        {
                                                ""position"":3,
                                           ""team"":{
                                                    ""id"":61,
                                              ""name"":""Chelsea FC"",
                                              ""crestUrl"":""https://crests.football-data.org/61.svg""
                                           },
                                           ""playedGames"":24,
                                           ""form"":null,
                                           ""won"":13,
                                           ""draw"":8,
                                           ""lost"":3,
                                           ""points"":47,
                                           ""goalsFor"":48,
                                           ""goalsAgainst"":18,
                                           ""goalDifference"":30
                                        },
                                        {
                                                ""position"":4,
                                           ""team"":{
                                                    ""id"":66,
                                              ""name"":""Manchester United FC"",
                                              ""crestUrl"":""https://crests.football-data.org/66.svg""
                                           },
                                           ""playedGames"":22,
                                           ""form"":null,
                                           ""won"":11,
                                           ""draw"":5,
                                           ""lost"":6,
                                           ""points"":38,
                                           ""goalsFor"":36,
                                           ""goalsAgainst"":30,
                                           ""goalDifference"":6
                                        },
                                        {
                                                ""position"":5,
                                           ""team"":{
                                                    ""id"":563,
                                              ""name"":""West Ham United FC"",
                                              ""crestUrl"":""https://crests.football-data.org/563.svg""
                                           },
                                           ""playedGames"":23,
                                           ""form"":null,
                                           ""won"":11,
                                           ""draw"":4,
                                           ""lost"":8,
                                           ""points"":37,
                                           ""goalsFor"":41,
                                           ""goalsAgainst"":31,
                                           ""goalDifference"":10
                                        },
                                        {
                                                ""position"":6,
                                           ""team"":{
                                                    ""id"":57,
                                              ""name"":""Arsenal FC"",
                                              ""crestUrl"":""https://crests.football-data.org/57.svg""
                                           },
                                           ""playedGames"":21,
                                           ""form"":null,
                                           ""won"":11,
                                           ""draw"":3,
                                           ""lost"":7,
                                           ""points"":36,
                                           ""goalsFor"":33,
                                           ""goalsAgainst"":25,
                                           ""goalDifference"":8
                                        },
                                        {
                                                ""position"":7,
                                           ""team"":{
                                                    ""id"":73,
                                              ""name"":""Tottenham Hotspur FC"",
                                              ""crestUrl"":""https://crests.football-data.org/73.svg""
                                           },
                                           ""playedGames"":20,
                                           ""form"":null,
                                           ""won"":11,
                                           ""draw"":3,
                                           ""lost"":6,
                                           ""points"":36,
                                           ""goalsFor"":26,
                                           ""goalsAgainst"":24,
                                           ""goalDifference"":2
                                        },
                                        {
                                                ""position"":8,
                                           ""team"":{
                                                    ""id"":76,
                                              ""name"":""Wolverhampton Wanderers FC"",
                                              ""crestUrl"":""https://crests.football-data.org/76.svg""
                                           },
                                           ""playedGames"":21,
                                           ""form"":null,
                                           ""won"":10,
                                           ""draw"":4,
                                           ""lost"":7,
                                           ""points"":34,
                                           ""goalsFor"":19,
                                           ""goalsAgainst"":16,
                                           ""goalDifference"":3
                                        },
                                        {
                                                ""position"":9,
                                           ""team"":{
                                                    ""id"":397,
                                              ""name"":""Brighton & Hove Albion FC"",
                                              ""crestUrl"":""https://crests.football-data.org/397.svg""
                                           },
                                           ""playedGames"":22,
                                           ""form"":null,
                                           ""won"":6,
                                           ""draw"":12,
                                           ""lost"":4,
                                           ""points"":30,
                                           ""goalsFor"":23,
                                           ""goalsAgainst"":23,
                                           ""goalDifference"":0
                                        },
                                        {
                                                ""position"":10,
                                           ""team"":{
                                                    ""id"":338,
                                              ""name"":""Leicester City FC"",
                                              ""crestUrl"":""https://crests.football-data.org/338.svg""
                                           },
                                           ""playedGames"":20,
                                           ""form"":null,
                                           ""won"":7,
                                           ""draw"":5,
                                           ""lost"":8,
                                           ""points"":26,
                                           ""goalsFor"":34,
                                           ""goalsAgainst"":37,
                                           ""goalDifference"":-3
                                        },
                                        {
                                                ""position"":11,
                                           ""team"":{
                                                    ""id"":58,
                                              ""name"":""Aston Villa FC"",
                                              ""crestUrl"":""https://crests.football-data.org/58.svg""
                                           },
                                           ""playedGames"":21,
                                           ""form"":null,
                                           ""won"":8,
                                           ""draw"":2,
                                           ""lost"":11,
                                           ""points"":26,
                                           ""goalsFor"":28,
                                           ""goalsAgainst"":32,
                                           ""goalDifference"":-4
                                        },
                                        {
                                                ""position"":12,
                                           ""team"":{
                                                    ""id"":340,
                                              ""name"":""Southampton FC"",
                                              ""crestUrl"":""https://crests.football-data.org/340.svg""
                                           },
                                           ""playedGames"":22,
                                           ""form"":null,
                                           ""won"":5,
                                           ""draw"":10,
                                           ""lost"":7,
                                           ""points"":25,
                                           ""goalsFor"":26,
                                           ""goalsAgainst"":34,
                                           ""goalDifference"":-8
                                        },
                                        {
                                                ""position"":13,
                                           ""team"":{
                                                    ""id"":354,
                                              ""name"":""Crystal Palace FC"",
                                              ""crestUrl"":""https://crests.football-data.org/354.svg""
                                           },
                                           ""playedGames"":22,
                                           ""form"":null,
                                           ""won"":5,
                                           ""draw"":9,
                                           ""lost"":8,
                                           ""points"":24,
                                           ""goalsFor"":31,
                                           ""goalsAgainst"":34,
                                           ""goalDifference"":-3
                                        },
                                        {
                                                ""position"":14,
                                           ""team"":{
                                                    ""id"":402,
                                              ""name"":""Brentford FC"",
                                              ""crestUrl"":""https://crests.football-data.org/402.svg""
                                           },
                                           ""playedGames"":23,
                                           ""form"":null,
                                           ""won"":6,
                                           ""draw"":5,
                                           ""lost"":12,
                                           ""points"":23,
                                           ""goalsFor"":26,
                                           ""goalsAgainst"":38,
                                           ""goalDifference"":-12
                                        },
                                        {
                                                ""position"":15,
                                           ""team"":{
                                                    ""id"":341,
                                              ""name"":""Leeds United FC"",
                                              ""crestUrl"":""https://crests.football-data.org/341.svg""
                                           },
                                           ""playedGames"":21,
                                           ""form"":null,
                                           ""won"":5,
                                           ""draw"":7,
                                           ""lost"":9,
                                           ""points"":22,
                                           ""goalsFor"":24,
                                           ""goalsAgainst"":40,
                                           ""goalDifference"":-16
                                        },
                                        {
                                                ""position"":16,
                                           ""team"":{
                                                    ""id"":62,
                                              ""name"":""Everton FC"",
                                              ""crestUrl"":""https://crests.football-data.org/62.svg""
                                           },
                                           ""playedGames"":20,
                                           ""form"":null,
                                           ""won"":5,
                                           ""draw"":4,
                                           ""lost"":11,
                                           ""points"":19,
                                           ""goalsFor"":24,
                                           ""goalsAgainst"":35,
                                           ""goalDifference"":-11
                                        },
                                        {
                                                ""position"":17,
                                           ""team"":{
                                                    ""id"":68,
                                              ""name"":""Norwich City FC"",
                                              ""crestUrl"":""https://upload.wikimedia.org/wikipedia/en/8/8c/Norwich_City.svg""
                                           },
                                           ""playedGames"":22,
                                           ""form"":null,
                                           ""won"":4,
                                           ""draw"":4,
                                           ""lost"":14,
                                           ""points"":16,
                                           ""goalsFor"":13,
                                           ""goalsAgainst"":45,
                                           ""goalDifference"":-32
                                        },
                                        {
                                                ""position"":18,
                                           ""team"":{
                                                    ""id"":67,
                                              ""name"":""Newcastle United FC"",
                                              ""crestUrl"":""https://crests.football-data.org/67.svg""
                                           },
                                           ""playedGames"":21,
                                           ""form"":null,
                                           ""won"":2,
                                           ""draw"":9,
                                           ""lost"":10,
                                           ""points"":15,
                                           ""goalsFor"":21,
                                           ""goalsAgainst"":43,
                                           ""goalDifference"":-22
                                        },
                                        {
                                                ""position"":19,
                                           ""team"":{
                                                    ""id"":346,
                                              ""name"":""Watford FC"",
                                              ""crestUrl"":""https://crests.football-data.org/346.svg""
                                           },
                                           ""playedGames"":20,
                                           ""form"":null,
                                           ""won"":4,
                                           ""draw"":2,
                                           ""lost"":14,
                                           ""points"":14,
                                           ""goalsFor"":23,
                                           ""goalsAgainst"":40,
                                           ""goalDifference"":-17
                                        },
                                        {
                                                ""position"":20,
                                           ""team"":{
                                                    ""id"":328,
                                              ""name"":""Burnley FC"",
                                              ""crestUrl"":""https://crests.football-data.org/328.svg""
                                           },
                                           ""playedGames"":18,
                                           ""form"":null,
                                           ""won"":1,
                                           ""draw"":9,
                                           ""lost"":8,
                                           ""points"":12,
                                           ""goalsFor"":16,
                                           ""goalsAgainst"":27,
                                           ""goalDifference"":-11
                                        }
                                     ]
                                  }
                               ]
                            }";

            //var httpAPIClient = GetTestHttpAPIClient(configuration);
            var httpAPIClient = new Mock<IHttpAPIClient>();
            httpAPIClient.Setup(x => x.Get(url)).ReturnsAsync(testJson);

            //var footballDataService = new FootballDataService(httpAPIClient, _mockTimeZoneOffsetService.Object, configuration, _mockFootballDataState.Object);

            //var mockFootballDataStandings = new Mock<FootballDataModel>();
            //mockFootballDataStandings.Setup(x => x).Returns(new FootballDataModel());

            //footballDataService.


            //var groups = await footballDataService.GetGroups();

            var mockFootballDataService = new Mock<IFootballDataService>();
            //mockFootballDataService.Setup(x => x.GetFootballDataStandings()).ReturnsAsync(GetTestGroups());
            mockFootballDataService.Setup(x => x.GetFixturesAndResultsByGroups(GetTestGroupsOrLeagueTables())).ReturnsAsync(GetTestGroupsOrLeagueTables());
            mockFootballDataService.Setup(x => x.GetGroupsOrLeagueTable()).ReturnsAsync(GetTestGroupsOrLeagueTables());

            var footballDataService = new FootballDataService(httpAPIClient.Object, _mockTimeZoneOffsetService.Object, configuration, _mockFootballDataState.Object);
            var groups = await footballDataService.GetGroupsOrLeagueTable();

            Assert.AreEqual(groups, GetTestGroupsOrLeagueTables());
            //mockFootballDataService.Setup(x => x.GetGroups());
        }

        private List<GroupOrLeagueTableModel> GetTestGroupsOrLeagueTables()
        {
            return new List<GroupOrLeagueTableModel>()
            {
                new GroupOrLeagueTableModel()
                {
                    Name = "A"
                },
                new GroupOrLeagueTableModel()
                {
                    Name = "B"
                },
                new GroupOrLeagueTableModel()
                {
                    Name = "C"
                },
            };
        }

        private IConfiguration GetTestConfiguration()
        {
            var inMemorySettings = new Dictionary<string, string> {
                                                {"FootballDataAPIURL", "http://www.foo.com/"},
                                                {"Competition", "PL" }
                                            };

            return new ConfigurationBuilder()
                            .AddInMemoryCollection(inMemorySettings)
                            .Build();
        }

        private HttpAPIClient GetTestHttpAPIClient(IConfiguration configuration)
        {
            var httpClient = new HttpClient();

            return new HttpAPIClient(httpClient, configuration);
        }
    }
}
