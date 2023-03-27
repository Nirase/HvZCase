using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models
{
    public class Kill
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public int? GameId { get; set; }
        public Game? Game { get; set; }
        [MaxLength(50)]
        public string TimeOfDeath { get; set; }
        [MaxLength(100)]
        public string? Location { get; set; }
        public int VictimId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Player Victim { get; set; }
        public int KillerId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Player Killer { get; set; } 
    }
}
