﻿namespace FootballShared.Models;

public class GroupOrLeagueTableStanding
{
    public int Position { get; set; }
    public Team Team { get; set; }
    public int GamesPlayed { get; set; }
    public int GamesWon { get; set; }
    public int GamesDrawn { get; set; }
    public int GamesLost { get; set; }
    public int GoalsScored { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalDifference { get; set; }
    public int PointsTotal { get; set; }
    public string Form { get; set; }
    public int? ThirdRankingPosition { get; set; }
}
