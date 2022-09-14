using static FootballShared.Models.Enums;

namespace FootballShared.Models
{
    public class PostGroupDraw
    {
        public Stage Stage { get; set; }
        public DateTime FixtureDate { get; set; }
        public string HomeDraw { get; set; }
        public string AwayDraw { get; set; }
    }
}
