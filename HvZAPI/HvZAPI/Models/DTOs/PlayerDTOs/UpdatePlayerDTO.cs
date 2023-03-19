namespace HvZAPI.Models.DTOs.PlayerDTOs
{
    public class UpdatePlayerDTO
    {
        public int Id { get; set; }
        public bool IsHuman { get; set; }
        public bool IsPatientZero { get; set; }
        public int SquadId { get; set; }

    }
}
