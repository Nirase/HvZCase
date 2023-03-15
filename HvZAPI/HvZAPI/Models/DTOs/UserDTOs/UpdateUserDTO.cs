using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models.DTOs.UserDTOs
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }
    }
}
