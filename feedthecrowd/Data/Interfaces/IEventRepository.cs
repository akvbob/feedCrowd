using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedTheCrowd.Models;

namespace FeedTheCrowd.Data.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> Add(Event recipe);
        Task<ICollection<Event>> GetByUser(int id);
        Task<Event> GetById(int id);
        Task<bool> Update(int id, Event newEve);
        Task<bool> AddRecipeToEvent(EventRecipe rec);
        Task<bool> RemoveRecipeFromEvent(EventRecipe rec);
        Task<string> Delete(int id);
    }
}
