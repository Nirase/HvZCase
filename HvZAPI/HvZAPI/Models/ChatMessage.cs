namespace HvZAPI.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public Chat Chat { get; set; }
        public string Channel { get; set; }
        public int GameId { get; set; }
        public Player Sender { get; set; }
        public int PlayerId { get; set; }
        public string Contents { get; set; }
    }
}
