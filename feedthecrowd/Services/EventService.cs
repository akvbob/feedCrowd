using AutoMapper;
using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.Products;
using FeedTheCrowd.Dtos.Events;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;
        public EventService(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<string> Delete(int id)
        {
            var success = await _repository.Delete(id);
            return success;
        }
        public async Task AddRecipeToEvent(int recipeId, int eventId)
        {
            if(recipeId.ToString().Equals("") || eventId.ToString().Equals(""))
            {
                throw new ArgumentNullException();
            }
            var ev = new EventRecipe();
            ev.FkRecipe = recipeId;
            ev.FkEvent = eventId;
            await _repository.AddRecipeToEvent(ev);
        }
        public async Task RemoveRecipeFromEvent(int recipeId, int eventId)
        {
            if (recipeId.ToString().Equals("") || eventId.ToString().Equals(""))
            {
                throw new ArgumentNullException();
            }
            var ev = new EventRecipe();
            ev.FkRecipe = recipeId;
            ev.FkEvent = eventId;
            await _repository.RemoveRecipeFromEvent(ev);
        }
        public async Task<EventDto> Create(NewEventDto newItem)
        {
            if (newItem == null)
                throw new ArgumentNullException();

            var ev = _mapper.Map<Event>(newItem);
            ev.DateCreated = DateTime.Now;
            await _repository.Add(ev);

            var evDto = _mapper.Map<EventDto>(ev);
            return evDto;
        }

        public async Task<EventDto> GetById(int id)
        {
            var ev = await _repository.GetById(id);
            var evDto = _mapper.Map<EventDto>(ev);
            return evDto;
        }

        public async Task<ICollection<EventDto>> GetByUser(int id)
        {
            var events = await _repository.GetByUser(id);
            var eventDtos = events.Select(c => _mapper.Map<EventDto>(c)).ToArray();

            return eventDtos;
        }
        public async Task Update(int id, NewEventDto evDto)
        {
            if (evDto == null)
                throw new ArgumentNullException();

            var itemToUpdate = await _repository.GetById(id);
            if (itemToUpdate == null)
            {
                throw new InvalidOperationException($"Event with {id} id was not found");
            }
            var ev = _mapper.Map<Event>(evDto);
            await _repository.Update(id, ev);
        }
    }
}
