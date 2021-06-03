using Euro2020BlazorApp.Models;
using Euro2020BlazorApp.Models.FootballData;
using System;
using System.Collections.Generic;
using System.Linq;
using static Euro2020BlazorApp.Models.Enums.Enums;

namespace Euro2020BlazorApp.Data
{
    public class FixtureService
    {
        Model _model;
        
        public FixtureService(Model model)
        {
            _model = model;
        }

        public List<Fixture> GetFixtures()
        {
            return _model
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
                            FixtureDate = x.utcDate,
                            Stage = GetStage(x.stage),
                            Group = new Group()
                            {
                                Name = x.group,
                            },
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
                case "ROUND_OF_16":
                    stage = Stage.RoundOf16;
                    break;
                case "QUARTER_FINAL":
                    stage = Stage.QuarterFinal;
                    break;
                case "SEMI_FINAL":
                    stage = Stage.SemiFinal;
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
