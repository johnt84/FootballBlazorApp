namespace FootballShared.Models
{
    public class Enums
    {
        public enum Stage
        {
            Group,
            Round_of_16,
            Quarter_Final,
            Semi_Final,
            Third_Place,
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

        public enum Confederation
        {
            Africa,
            Asia,
            Europe,
            NorthAndCentralAmerica,
            Carribean,
            Oceania,
            SouthAmerica,
        }
    }
}
