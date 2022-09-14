using static FootballShared.Models.Enums;

namespace FootballShared.Models
{
    public class CountriesInConfederation
    {
        public Confederation Confederation { get; set; }
        public string ConfederationForDisplay { get; set; }
        public List<string> Countries { get; set; }
    }
}
