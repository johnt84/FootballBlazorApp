namespace FootballShared.Models.FootballData
{
    public class FootballDataModel
    {
        public int count { get; set; }
        public Filters filters { get; set; }
        public Competition competition { get; set; }
        public Season season { get; set; }
        public Standing[] standings { get; set; }
        public Match[] matches { get; set; }
    }

    public class Filters
    {
    }

    public class Competition
    {
        public int id { get; set; }
        public Area area { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string plan { get; set; }
        public DateTime lastUpdated { get; set; }
    }

    public class Area
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Season
    {
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int currentMatchday { get; set; }
        public object winner { get; set; }
    }

    public class Match
    {
        public int id { get; set; }
        public Season season { get; set; }
        public DateTime utcDate { get; set; }
        public string status { get; set; }
        public int matchday { get; set; }
        public string stage { get; set; }
        public string group { get; set; }
        public DateTime lastUpdated { get; set; }
        public Odds odds { get; set; }
        public Score score { get; set; }
        public Hometeam homeTeam { get; set; }
        public Awayteam awayTeam { get; set; }
        public object[] referees { get; set; }
    }

    public class Standing
    {
        public string stage { get; set; }
        public string type { get; set; }
        public string group { get; set; }
        public Table[] table { get; set; }
    }

    public class Table
    {
        public int position { get; set; }
        public Team team { get; set; }
        public int playedGames { get; set; }
        public object form { get; set; }
        public int won { get; set; }
        public int draw { get; set; }
        public int lost { get; set; }
        public int points { get; set; }
        public int goalsFor { get; set; }
        public int goalsAgainst { get; set; }
        public int goalDifference { get; set; }
    }

    public class Teams
    {
        public int count { get; set; }
        public Filters filters { get; set; }
        public Competition competition { get; set; }
        public Season season { get; set; }
        public Team[] teams { get; set; }
    }

    public class Team
    {
        public Area area { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string tla { get; set; }
        public string crest { get; set; }
        public string address { get; set; }
        public string website { get; set; }
        public int founded { get; set; }
        public string clubColors { get; set; }
        public string venue { get; set; }

        public Coach coach { get; set; }
        public Squad[] squad { get; set; }
        public DateTime lastUpdated { get; set; }
    }

    public class Coach
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string name { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string nationality { get; set; }
    }

    public class Odds
    {
        public string msg { get; set; }
    }

    public class Score
    {
        public string winner { get; set; }
        public string duration { get; set; }
        public Fulltime fullTime { get; set; }
        public Halftime halfTime { get; set; }
        public Extratime extraTime { get; set; }
        public Penalties penalties { get; set; }
    }

    public class Fulltime
    {
        public int? home { get; set; }
        public int? away { get; set; }
    }

    public class Halftime
    {
        public int? home { get; set; }
        public int? away { get; set; }
    }

    public class Extratime
    {
        public int? home { get; set; }
        public int? away { get; set; }
    }

    public class Penalties
    {
        public int? homeTeam { get; set; }
        public int? awayTeam { get; set; }
    }

    public class Hometeam
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
    }

    public class Awayteam
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
    }

    public class Activecompetition
    {
        public int id { get; set; }
        public Area area { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string plan { get; set; }
        public DateTime lastUpdated { get; set; }
    }

    public class Squad
    {
        public int id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string nationality { get; set; }
    }
}
