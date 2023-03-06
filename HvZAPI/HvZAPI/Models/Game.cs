namespace HvZAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GameState { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<Kill> Kills { get; set; }

    }
}
