using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models.DTOs.UserDTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        public List<string> Players { get; set; }
    }
}
