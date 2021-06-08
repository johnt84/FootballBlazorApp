using System;
using System.Collections.Generic;
using System.Linq;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var fixturesAndResults = new List<FixtureAndResult>()
            {
                new FixtureAndResult()
                {
                    GameStatus = GameStatus.Scheduled,
                    HomeTeam = new Team()
                    {
                        Name = "England",
                    },
                    AwayTeam = new Team()
                    {
                        Name = "Croatia",
                    },
                    FixtureDate = new DateTime(2021,6,13,14,0,0),
                    Stage = Stage.Group,
                    Group = new Group()
                    {
                        Name = "Group D",
                    },
                },
                new FixtureAndResult()
                {
                    GameStatus = GameStatus.Scheduled,
                    HomeTeam = new Team()
                    {
                        Name = "Scotland",
                    },
                    AwayTeam = new Team()
                    {
                        Name = "Czech Republic",
                    },
                    FixtureDate = new DateTime(2021,6,14,14,0,0),
                    Stage = Stage.Group,
                    Group = new Group()
                    {
                        Name = "Group D",
                    },
                },
               new FixtureAndResult()
                {
                    GameStatus = GameStatus.Scheduled,
                    HomeTeam = new Team()
                    {
                        Name = "Croatia",
                    },
                    AwayTeam = new Team()
                    {
                        Name = "Czech Republic",
                    },
                    FixtureDate = new DateTime(2021,6,18,17,0,0),
                    Stage = Stage.Group,
                    Group = new Group()
                    {
                        Name = "Group D",
                    },
                },
                new FixtureAndResult()
                {
                    GameStatus = GameStatus.Scheduled,
                    HomeTeam = new Team()
                    {
                        Name = "England",
                    },
                    AwayTeam = new Team()
                    {
                        Name = "Scotland",
                    },
                    FixtureDate = new DateTime(2021,6,18,20,0,0),
                    Stage = Stage.Group,
                    Group = new Group()
                    {
                        Name = "Group D",
                    },
                },
            };

            const string GROUP = "Group";
            string groupName = "D";
            
            var fixturesAndResultsByGroup = fixturesAndResults
                                                .Where(x => x.Group.Name == $"{GROUP} {groupName}")
                                                .GroupBy(x => x.Group.Name).Select(x => new FixturesAndResultsByGroup()
                                                {
                                                    GroupName = x.Key,
                                                    FixturesAndResults = x.ToList(),
                                                })
                                                .FirstOrDefault();

            var fixturesAndResultsByDay = fixturesAndResultsByGroup
                                            .FixturesAndResults
                                            .GroupBy(x => x.FixtureDate.Date)
                                            .Select(x => new FixturesAndResultsByDay() { FixtureDate = x.Key, FixturesAndResults = x.ToList(), })
                                            .ToList();

            Console.WriteLine("Hello World!");
        }
    }

    public enum Stage
    {
        Group,
        Round_of_16,
        Quarter_Final,
        Semi_Final,
        Final,
    }

    public enum Result
    {
        Home_Win,
        Away_Win,
        Draw,
    }

    public enum GameStatus
    {
        Scheduled,
        In_Play,
        Result,
    }

    public class Team
    {
        public string Name { get; set; }
        public string FlagIcon { get; set; }
        public Stage StageReached { get; set; }
    }

    public class GroupStanding
    {
        public Team Team { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesDrawn { get; set; }
        public int GamesLost { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int PointsTotal { get; set; }
    }

    public class FixturesAndResultsByDay
    {
        public DateTime FixtureDate { get; set; }
        public List<FixtureAndResult> FixturesAndResults { get; set; }
    }

    public class FixturesAndResultsByGroup
    {
        public string GroupName { get; set; }
        public Group Group { get; set; }
        public List<FixturesAndResultsByDay> FixturesAndResultsByDays { get; set; }
        public List<FixtureAndResult> FixturesAndResults { get; set; }
    }

    public class Group
    {
        public string Name { get; set; }
        public List<GroupStanding> GroupStandings { get; set; }
        public FixturesAndResultsByGroup FixturesAndResultsByGroup { get; set; }
    }

    public class FixtureAndResult
    {
        public GameStatus GameStatus { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public DateTime FixtureDate { get; set; }
        public Stage Stage { get; set; }
        public Group Group { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public Result Result { get; set; }
    }
}
