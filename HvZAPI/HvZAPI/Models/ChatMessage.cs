namespace HvZAPI.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public Channel Channel { get; set; }
        public int SenderId { get; set; }
        public Player Sender { get; set; }
        public string Contents { get; set; }
    }
}
/*
 * Find channel by name instead of Id
*/