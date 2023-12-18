namespace FootballWebApplication.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public string? Gender { get; set; }
        public string? TeamName { get; set; }
        public string? Country { get; set; }
        public DateTime DateBirthday { get; set; }

    }
}
