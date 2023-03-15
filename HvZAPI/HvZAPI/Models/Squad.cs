namespace HvZAPI.Models
{
    public class Squad
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<SquadCheckIn> SquadCheckIns { get; set; }
    }
}
