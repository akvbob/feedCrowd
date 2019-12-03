using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedTheCrowd.Dtos.Events;

namespace FeedTheCrowd.Services.Interfaces
{
    public interface IEventService
    {
        //Task<ICollection<AllRecipesDto>> GetAll();
        Task<EventDto> Create(NewEventDto newItem);
        Task<ICollection<EventDto>> GetByUser(int userId);
        Task<EventDto> GetById(int id);
        Task<string> Delete(int id);
        Task Update(int id, NewEventDto newEv);
        Task AddRecipeToEvent(int recipeId, int eventId);
        Task RemoveRecipeFromEvent(int recipeId, int eventId);
    }
}
