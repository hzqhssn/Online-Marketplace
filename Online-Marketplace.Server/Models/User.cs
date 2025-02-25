using System.ComponentModel.DataAnnotations;

namespace OnlineMarketplace.Server.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
