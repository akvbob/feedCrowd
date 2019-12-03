using FeedTheCrowd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedTheCrowd.Dtos.Auth;
using FeedTheCrowd.Dtos.User;

namespace FeedTheCrowd.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> Register(UserForRegistrationDto userForRegistrationDto);
        Task<User> Login(UserForLoginDto userForLoginDto);
    }
}
