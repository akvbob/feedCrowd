using AutoMapper;
using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.Products;
using FeedTheCrowd.Dtos.Recipes;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;
        private readonly IMapper _mapper;
        public RecipeService(IRecipeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<RecipeDto> Create(NewRecipeDto newItem)
        {
            if (newItem == null)
                throw new ArgumentNullException();

            var recipe = _mapper.Map<Recipe>(newItem);
            await _repository.Add(recipe);

            var recipeDto = _mapper.Map<RecipeDto>(newItem);
            return recipeDto;
        }
        public async Task<ICollection<AllRecipesDto>> GetAll()
        {
            var recipes = await _repository.GetAll();
            var recipesDtos = _mapper.Map<AllRecipesDto[]>(recipes);
            return recipesDtos;
        }
        public async Task<ICollection<AllRecipesDto>> GetRecipesByEvent(int id)
        {
            var recipes = await _repository.GetRecipesByEvent(id);
            var recipesDtos = _mapper.Map<AllRecipesDto[]>(recipes);
            return recipesDtos;
        }
        public async Task<ICollection<AllRecipesDto>> GetRecipesByUser(int id)
        {
            var recipes = await _repository.GetRecipesByUser(id);
            var recipesDtos = _mapper.Map<AllRecipesDto[]>(recipes);
            return recipesDtos;
        }
        public async Task<RecipeDto> GetById(int id)
        {
            var recipe = await _repository.GetById(id);
            var recipeDto = _mapper.Map<RecipeDto>(recipe);
            return recipeDto;
        }
        public async Task<string> Delete(int id)
        {
            var success = await _repository.Delete(id);
            return success;
        }
        public async Task Update(int id, NewRecipeDto recipe)
        {
            if (recipe == null)
                throw new ArgumentNullException();

            var itemToUpdate = await _repository.GetById(id);
            if (itemToUpdate == null)
            {
                throw new InvalidOperationException($"Recipe with {id} id was not found");
            }
            
            await _repository.Update(itemToUpdate, recipe);
        }
    }
}
