using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.User;
using FeedTheCrowd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FeedTheCrowdContext _dataContext;

        public UserRepository(FeedTheCrowdContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<ICollection<User>> GetAll()
        {
            var values = await _dataContext.User.ToListAsync();
            return values;
        }

        public async Task<User> GetById(int id)
        {
            return await _dataContext.User.FindAsync(id);
        }
        public async Task<bool> Delete(int id)
        {
            var value = await _dataContext.User.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (value == null)
            {
                return false;
            }
            var values = _dataContext.User.Remove(value);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        public async Task<User> Update(User user, UserDto newUser)
        {
            var findUser = await _dataContext.User.FirstOrDefaultAsync(x => x.Id.Equals(user.Id));
            if (findUser == null)
            {
                throw new InvalidOperationException($"User id: {user.Id} was not found");
            }
            
            findUser.Name = newUser.Name;
            findUser.Surname = newUser.Surname;
            findUser.Email = newUser.Email;
            findUser.Photo = newUser.Photo;
            findUser.IsAdmin = newUser.IsAdmin;
            _dataContext.Update(findUser);
            await _dataContext.SaveChangesAsync();

            return findUser;
        }
    }
}
