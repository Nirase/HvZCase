namespace HvZAPI.Models
{
    public class Player
    {
        public int Id { get; set; }
        public bool IsHuman { get; set; }
        public bool IsPatientZero { get; set; }
        public string BiteCode { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        ICollection<ChatMessage> SentMessages { get; set; }
    }
}
