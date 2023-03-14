namespace HvZAPI.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public Channel Channel { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public string Contents { get; set; }
    }
}
/*
 * Find channel by name instead of Id
*/