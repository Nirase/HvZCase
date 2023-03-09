namespace HvZAPI.Models.DTOs.PlayerDTOs
{
    public class LightweightPlayerDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsHuman { get; set; }
    }
}
