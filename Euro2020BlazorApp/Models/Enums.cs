namespace Euro2020BlazorApp.Models.Enums
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

        public enum FixtureSource
        {
            FixturesByDay,
            FixturesByGroup,
        }
    }
}
