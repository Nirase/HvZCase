namespace HvZAPI.Models.DTOs.KillDTOs
{
    public class CreateKillDTO
    {
        public int GameId { get; set; }
        public string? Description { get; set; }
        public string TimeOfDeath { get; set; }
        public string? Location { get; set; }
        public string BiteCode { get; set; }
        public int KillerId { get; set; }
    }
}
