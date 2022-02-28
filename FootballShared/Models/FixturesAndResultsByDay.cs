namespace FootballShared.Models
{
    public class FixturesAndResultsByDay
    {
        public DateTime FixtureDate { get; set; }
        public List<FixtureAndResult> FixturesAndResults { get; set; }
    }
}
