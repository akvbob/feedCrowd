using FeedTheCrowd.Dtos.Products;
using FeedTheCrowd.Dtos.Recipes;
using FeedTheCrowd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Data.Interfaces
{
    public interface IRecipeRepository
    {
        Task<ICollection<Recipe>> GetAll();
        Task<ICollection<Recipe>> GetRecipesByEvent(int id);
        Task<ICollection<Recipe>> GetRecipesByUser(int id);
        Task<Recipe> Add(Recipe recipe);
        Task<Recipe> GetById(int id);
        Task<string> Delete(int id);
        Task<bool> Update(Recipe recipe, NewRecipeDto newRecipe);


    }
}
