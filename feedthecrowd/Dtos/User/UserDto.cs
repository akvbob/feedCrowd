using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Dtos.User
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Photo { get; set; }
        public bool IsAdmin { get; set; }
    }
}
