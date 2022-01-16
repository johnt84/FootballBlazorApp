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

            var httpAPIClient = GetTestHttpAPIClient(configuration);

            //var footballDataService = new FootballDataService(httpAPIClient, _mockTimeZoneOffsetService.Object, configuration, _mockFootballDataState.Object);

            //var mockFootballDataStandings = new Mock<FootballDataModel>();
            //mockFootballDataStandings.Setup(x => x).Returns(new FootballDataModel());

            //footballDataService.


            //var groups = await footballDataService.GetGroups();

            var mockFootballDataService = new Mock<IFootballDataService>();
            //mockFootballDataService.Setup(x => x.GetFootballDataStandings()).ReturnsAsync(GetTestGroups());
            mockFootballDataService.Setup(x => x.GetFixturesAndResultsByGroups(GetTestGroupsOrLeagueTables())).ReturnsAsync(GetTestGroupsOrLeagueTables());
            mockFootballDataService.Setup(x => x.GetGroupsOrLeagueTable()).ReturnsAsync(GetTestGroupsOrLeagueTables());

            var footballDataService = new FootballDataService(httpAPIClient, _mockTimeZoneOffsetService.Object, configuration, _mockFootballDataState.Object);
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
