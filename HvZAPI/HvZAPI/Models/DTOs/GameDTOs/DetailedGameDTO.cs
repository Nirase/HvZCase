using HvZAPI.Models.DTOs.KillDTOs;
using HvZAPI.Models.DTOs.PlayerDTOs;

namespace HvZAPI.Models.DTOs.GameDTOs
{
    public class DetailedGameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GameState { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<LightweightPlayerDTO> Players { get; set; }
        public List<LightweightKillDTO> Kills { get; set; }
    }
}
