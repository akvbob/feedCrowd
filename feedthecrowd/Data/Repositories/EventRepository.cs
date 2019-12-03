using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.Events;
using FeedTheCrowd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Data.Repositories
{
    public class EventRepository :IEventRepository
    {
        private readonly FeedTheCrowdContext _dataContext;

        public EventRepository(FeedTheCrowdContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Event> Add(Event ev)
        {
            await _dataContext.AddAsync(ev);
            await _dataContext.SaveChangesAsync();
            return ev;
        }

        public async Task<ICollection<Event>> GetByUser(int id)
        {
            var values = await _dataContext.Event.Where(x => x.FkUser.Equals(id)).ToListAsync() ;
            return values;
        }

        public async Task<Event> GetById(int id)
        {
            var values = await _dataContext.Event.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return values;
        }
        public async Task<bool> Update(int id, Event newEv)
        {
            var ev = await _dataContext.Event.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (ev == null)
            {
                throw new InvalidOperationException($"Item {id} was not found");
            }

            if (newEv.Title != null || newEv.Title != "")  ev.Title = newEv.Title ;
            if (newEv.EventStartDate != null) ev.EventStartDate = newEv.EventStartDate;
            if (newEv.EventEndDate != null) ev.EventEndDate = newEv.EventEndDate;
            if (newEv.PeopleCount > 0) ev.PeopleCount = newEv.PeopleCount;
            
            _dataContext.Update(ev);
            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddRecipeToEvent(EventRecipe rec)
        {
            if(rec == null)
            {
                throw new InvalidOperationException($"Was not found");
            }
            await _dataContext.AddAsync(rec);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveRecipeFromEvent(EventRecipe rec)
        {
            if (rec == null)
            {
                throw new InvalidOperationException($"Was not found");
            }
            var item = _dataContext.EventRecipe.Where(x => x.FkRecipe == rec.FkRecipe && x.FkEvent == rec.FkEvent).FirstOrDefault();
            _dataContext.EventRecipe.Remove(item);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        public async Task<string> Delete(int id)
        {
            var value = await _dataContext.Event.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (value == null)
            {
                return null;
            }
            var recipes = await _dataContext.EventRecipe.Where(x => x.FkEvent.Equals(id)).ToListAsync();
            foreach (var item in recipes)
            {
                _dataContext.EventRecipe.Remove(item);
            }
            var values = _dataContext.Event.Remove(value);
            await _dataContext.SaveChangesAsync();

            return id.ToString();
        }
    }
}
