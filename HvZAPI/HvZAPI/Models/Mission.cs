using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models
{
    public class Mission
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        public bool VisibleToHumans { get; set; }
        public bool VisibleToZombies { get; set; }
        [MaxLength(10)]
        public string StartDate { get; set; }
        [MaxLength(10)]
        public string EndDate { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }
        public Game Game { get; set; }
        public int GameId { get; set; }
    }
}
