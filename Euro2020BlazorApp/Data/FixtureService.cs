using Euro2020BlazorApp.Models;
using Euro2020BlazorApp.Models.FootballData;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Euro2020BlazorApp.Models.Enums.Enums;

namespace Euro2020BlazorApp.Data
{
    public class FixtureService
    {
        private readonly FootballDataModel _footballDataModel;
        private readonly ITimeZoneOffsetService _timeZoneOffsetService;
        
        public FixtureService(FootballDataModel footballDataModel, ITimeZoneOffsetService timeZoneOffsetService)
        {
            _footballDataModel = footballDataModel;
            _timeZoneOffsetService = timeZoneOffsetService;
        }

        public async Task<List<FixturesByDay>> GetFixtures()
        {
            int timeZoneOffsetInMinutes = await _timeZoneOffsetService.GetLocalOffsetInMinutes();

            var fixtures = _footballDataModel
                                .matches
                                .ToList()
                                .Select(x => new Fixture()
                                {
                                    HomeTeam = new Models.Team()
                                    {
                                        Name = x.homeTeam.name,
                                    },
                                    AwayTeam = new Models.Team()
                                    {
                                        Name = x.awayTeam.name,
                                    },
                                    FixtureDate = x.utcDate.AddMinutes(-timeZoneOffsetInMinutes),
                                    Stage = GetStage(x.stage),
                                    Group = new Group()
                                    {
                                        Name = x.group,
                                    },
                                })
                                .ToList();

            return fixtures
                        .GroupBy(x => x.FixtureDate.Date).Select(x => new FixturesByDay()
                        {
                            FixtureDate = x.Key,
                            Fixtures = x.ToList(),
                        })
                        .ToList();
        }

        private Stage GetStage(string stageName)
        {
            var stage = Stage.Group;
            
            switch(stageName)
            {
                case "GROUP_STAGE":
                    stage = Stage.Group;
                    break;
                case "LAST_16":
                    stage = Stage.Round_of_16;
                    break;
                case "QUARTER_FINAL":
                    stage = Stage.Quarter_Final;
                    break;
                case "SEMI_FINAL":
                    stage = Stage.Semi_Final;
                    break;
                case "FINAL":
                    stage = Stage.Final;
                    break;
                default:
                    break;
            }

            return stage;
        }
    }
}
