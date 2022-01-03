﻿namespace FootballBlazorApp.Models.Enums
{
    public class Enums
    {
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

        public enum ComponentSource
        {
            FixturesAndResults,
            FixturesByGroup,
            Team,
        }

        public enum SquadRole
        {
            Player,
            Coach,
            Assistant_Coach,
        }

        public enum PlayerPosition
        {
            Goalkeeper,
            Defender,
            Midfielder,
            Attacker,
        }
    }
}