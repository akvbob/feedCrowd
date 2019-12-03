
using System.ComponentModel.DataAnnotations;


namespace FeedTheCrowd.Dtos.Auth
{
    public class UserForLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
