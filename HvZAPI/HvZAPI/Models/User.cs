using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public ICollection<Player> Players { get; set; }
    }
}
