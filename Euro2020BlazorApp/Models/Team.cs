using static Euro2020BlazorApp.Models.Enums.Enums;

namespace Euro2020BlazorApp.Models
{
    public class Team
    {
        public string Name { get; set; }
        public string FlagIcon { get; set; }
        public Stage StageReached { get; set; }
    }
}
