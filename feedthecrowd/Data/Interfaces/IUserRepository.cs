using FeedTheCrowd.Dtos.User;
using FeedTheCrowd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAll();
        Task<User> GetById(int id);
        Task<bool> Delete(int id);
        Task<User> Update(User user, UserDto newUser);
    }
}
