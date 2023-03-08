namespace HvZAPI.Models.DTOs.GameDTOs
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GameState { get; set; }
        public List<string> Players { get; set; }
        public List<string> Kills { get; set; }
    }
}
