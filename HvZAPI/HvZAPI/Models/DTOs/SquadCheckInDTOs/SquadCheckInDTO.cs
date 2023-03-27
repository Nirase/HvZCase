using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models.DTOs.SquadCheckInDTOs
{
    public class SquadCheckInDTO
    {
        public int Id { get; set; }
        public string Squad { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
