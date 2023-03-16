namespace HvZAPI.Models.DTOs.SquadCheckInDTOs
{
    public class CreateSquadCheckInDTO
    {

        public int SquadId { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
