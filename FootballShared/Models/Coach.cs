namespace FootballShared.Models
{
    public class Coach
    {
        public int CoachID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int Age { get; set; }
        public int TeamID { get; set; }
    }
}
