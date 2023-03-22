using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models
{
    public class Squad
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int ChannelId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Channel Channel { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<SquadCheckIn> SquadCheckIns { get; set; }
    }
}
