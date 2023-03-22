using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(2000)]
        public string? Description { get; set; }
        [MaxLength(20)]
        public string GameState { get; set; }
        [MaxLength(10)]
        public string StartDate { get; set; }
        [MaxLength(10)]
        public string EndDate { get; set; }
        public float Radius { get; set; }
        [MaxLength(100)]
        public string Location { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<Kill> Kills { get; set; }
        public ICollection<Mission> Missions { get; set; }
        public ICollection<Channel> Channels { get; set; }
    }
}
