using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public Channel Channel { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        [MaxLength(1000)]
        public string Contents { get; set; }
    }
}
/*
 * Find channel by name instead of Id
*/