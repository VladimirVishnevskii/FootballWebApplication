namespace FootballWebApplication.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string? TeamName { get; set; }

        List<Player> Players = new ();
    }
}
