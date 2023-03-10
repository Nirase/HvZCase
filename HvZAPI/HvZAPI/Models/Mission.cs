using Microsoft.Identity.Client;

namespace HvZAPI.Models
{
    public class Mission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool VisibleToHumans { get; set; }
        public bool VisibleToZombies { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Location { get; set; }
        public Game Game { get; set; }
        public int GameId { get; set; }
    }
}
