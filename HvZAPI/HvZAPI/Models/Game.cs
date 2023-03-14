namespace HvZAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GameState { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public float Radius { get; set; }
        public string Location { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<Kill> Kills { get; set; }
        public ICollection<Mission> Missions { get; set; }
        public ICollection<Channel> Channels { get; set; }
    }
}
