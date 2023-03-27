using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models
{
    public class Channel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public ICollection<ChatMessage> Messages { get; set; }
    }
}