using HvZAPI.Models.DTOs.PlayerDTOs;
using HvZAPI.Models.DTOs.SquadCheckInDTOs;

namespace HvZAPI.Models.DTOs.SquadDTOs
{
    public class DetailedSquadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Game { get; set; }
        public List<LightweightPlayerDTO> Players { get; set; }
        public List<SquadCheckInDTO> SquadCheckIns { get; set; }
    }
}
