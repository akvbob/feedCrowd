using FeedTheCrowd.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Services.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<AllUserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<bool> Delete(int id);
        Task<UserDto> Update(int id, UserDto user);
    }
}
