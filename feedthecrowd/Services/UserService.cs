using AutoMapper;
using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.User;
using FeedTheCrowd.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ICollection<AllUserDto>> GetAll()
        {
            var users = await _repository.GetAll();
            var usersDto = _mapper.Map<AllUserDto[]>(users);
            return usersDto;
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _repository.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async Task<bool> Delete(int id)
        {
            var success = await _repository.Delete(id);
            return success;
        }
        public async Task<UserDto> Update(int id, UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException();

            var itemToUpdate = await _repository.GetById(id);
            if (itemToUpdate == null)
            {
                throw new InvalidOperationException($"User with {id} id was not found");
            }
            var us = await _repository.Update(itemToUpdate, user);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
