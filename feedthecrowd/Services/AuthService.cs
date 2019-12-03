using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.Auth;
using FeedTheCrowd.Dtos.User;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;

namespace FeedTheCrowd.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IMapper _mapper;
        public AuthService(IAuthRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDto> Register(UserForRegistrationDto userForRegistrationDto)
        {
            var userToCreate = new User
            {
                Name = userForRegistrationDto.Name,
                Surname = userForRegistrationDto.Surname,
                Username = userForRegistrationDto.Username,
                Email = userForRegistrationDto.email,
            };

            var user = await _repository.Register(userToCreate, userForRegistrationDto.Password);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<User> Login(UserForLoginDto userForLoginDto)
        {
            return await _repository.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
        }
    }
}
