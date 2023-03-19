namespace HvZAPI.Models.DTOs.PlayerDTOs
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public bool IsHuman { get; set; }
        public bool IsPatientZero { get; set; }
        public string BiteCode { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public int GameId { get; set; }
        public string Game { get; set; }
        public int? SquadId { get; set; }
    }
}
