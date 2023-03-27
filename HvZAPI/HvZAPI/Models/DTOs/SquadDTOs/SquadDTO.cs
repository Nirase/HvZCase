namespace HvZAPI.Models.DTOs.SquadDTOs
{
    public class SquadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Game { get; set; }
        public List<string> Players { get; set; }
        public List<string> SquadCheckIns { get; set; }
    }
}
