using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Models
{ 

    [PrimaryKey(nameof(Channel), nameof(GameId))]
    public class Chat
    {
        public string Channel { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public ICollection<ChatMessage> Messages { get; set; }
    }
}
