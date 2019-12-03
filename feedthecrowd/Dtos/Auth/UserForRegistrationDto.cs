
using System.ComponentModel.DataAnnotations;

namespace FeedTheCrowd.Dtos.Auth
{
    public class UserForRegistrationDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 25 characters")]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string email { get; set; }
    }
}
