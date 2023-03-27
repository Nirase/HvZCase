namespace HvZAPI.Models.DTOs.PlayerDTOs
{
    public class CreatePlayerDTO
    {
        public bool IsHuman { get; set; }
        public bool IsPatientZero { get; set; }
        public string BiteCode { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}
