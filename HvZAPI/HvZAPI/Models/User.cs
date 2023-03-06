using System.ComponentModel.DataAnnotations;

namespace HvZAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [MaxLength(30)]
        public string first_name { get; set; }

        [MaxLength(30)]
        public string last_name { get; set; }

        ICollection<Player> players { get; set; }
    }
}
