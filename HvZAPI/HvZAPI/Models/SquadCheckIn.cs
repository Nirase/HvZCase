using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models
{
    public class SquadCheckIn
    {
        public int Id { get; set; }
        public int SquadId { get; set; }
        public Squad Squad { get; set; }
        [MaxLength(100)]
        public string Location { get; set; }
        [MaxLength(10)]
        public string StartDate { get; set; }
        
        [MaxLength(10)]
        public string EndDate { get; set; }
    }
}
