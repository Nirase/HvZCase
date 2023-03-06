namespace HvZAPI.Models
{
    public class Kill
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string TimeOfDeath { get; set; }
        public int KillerId { get; set; }
        public Player Killer { get; set; }
        public int VictimId { get; set; }
        public Player Victim { get; set; }
    }
}
