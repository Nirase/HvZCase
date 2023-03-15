using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models
{
    public class SquadCheckIn
    {
        public int Id { get; set; }
        public int SquadId { get; set; }
        public Squad Squad { get; set; }
        [MaxLength(150)]
        public string Location { get; set; }
    }
}
