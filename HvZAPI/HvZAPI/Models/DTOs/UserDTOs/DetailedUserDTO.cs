using HvZAPI.Models.DTOs.PlayerDTOs;
using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models.DTOs.UserDTOs
{
    public class DetailedUserDTO
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        public List<LightweightPlayerDTO> Players { get; set; }
    }
}
