using FeedTheCrowd.Dtos.Products;
using FeedTheCrowd.Dtos.Recipes;
using FeedTheCrowd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<ICollection<AllRecipesDto>> GetAll();
        Task<ICollection<AllRecipesDto>> GetRecipesByEvent(int id);
        Task<ICollection<AllRecipesDto>> GetRecipesByUser(int id);
        Task<RecipeDto> Create(NewRecipeDto newItem);
        Task<RecipeDto> GetById(int id);
        Task<string> Delete(int id);
        Task Update(int id, NewRecipeDto recipe);
    }
}
