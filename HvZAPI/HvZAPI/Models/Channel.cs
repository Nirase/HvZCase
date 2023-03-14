using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Models
{
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public ICollection<ChatMessage> Messages { get; set; }
    }
}

/*
 * On channel creation -> Check if name exists
 * If it does deny request
 */