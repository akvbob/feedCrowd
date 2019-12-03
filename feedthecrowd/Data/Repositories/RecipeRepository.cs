using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.Products;
using FeedTheCrowd.Dtos.Recipes;
using FeedTheCrowd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly FeedTheCrowdContext _dataContext;

        public RecipeRepository(FeedTheCrowdContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Recipe> Add(Recipe recipe)
        {

            var newRecipe = new Recipe
            {
                Image = recipe.Image,
                Servings = recipe.Servings,
                Time = recipe.Time,
                Title = recipe.Title,
                Description = recipe.Description,
                FkUser = recipe.FkUser
            };
            await _dataContext.AddAsync(newRecipe);
            await _dataContext.SaveChangesAsync();


            foreach (var item in recipe.Products)
            {
                var product = new Product
                {
                    Name = item.Name,
                    Quantity = item.Quantity,
                    FkRecipe = newRecipe.Id
                };
                await _dataContext.Product.AddAsync(product);
            }
           await _dataContext.SaveChangesAsync();

            return newRecipe;
        }

        public async Task<ICollection<Recipe>> GetAll()
        {
            var values = await _dataContext.Recipe.ToListAsync();
            return values;
        }
        public async Task<ICollection<Recipe>> GetRecipesByEvent(int id)
        {
            var ids = await _dataContext.EventRecipe.Where(x => x.FkEvent == id).Select(x => x.FkRecipe).ToListAsync();
            var recipes = await _dataContext.Recipe.Where(x => ids.Contains(x.Id)).ToListAsync();
            return recipes;
        }

        public async Task<ICollection<Recipe>> GetRecipesByUser(int id)
        {
            var recipes = await _dataContext.Recipe.Where(x => x.FkUser.Equals(id)).ToArrayAsync();
            return recipes;
        }

        public async Task<Recipe> GetById(int id)
        {
            return await _dataContext.Recipe.FindAsync(id);
        }
        public async Task<string> Delete(int id)
        {
            var value = await _dataContext.Recipe.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (value == null)
            {
                return null;
            }
            var eventRecipe = await _dataContext.EventRecipe.Where(x => x.FkRecipe.Equals(id)).ToArrayAsync();
            if(eventRecipe.Length != 0)
            {
                return "Recipe is in events";
            }
            var products = await _dataContext.Product.Where(x => x.FkRecipe.Equals(id)).ToListAsync();
            foreach (var item in products)
            {
                _dataContext.Product.Remove(item);
            }
            var values = _dataContext.Recipe.Remove(value);
            await _dataContext.SaveChangesAsync();

            return id.ToString();
        }
        public async Task<bool> Update(Recipe recipe, NewRecipeDto newRecipe)
        {
            var findRecipe = await _dataContext.Recipe.FirstOrDefaultAsync(x => x.Id.Equals(recipe.Id));
            if (findRecipe == null)
            {
                throw new InvalidOperationException($"Item {recipe.Id} was not found");
            }

            findRecipe.Description = newRecipe.Description;
            findRecipe.Title = newRecipe.Title;
            findRecipe.Time = newRecipe.Time;
            findRecipe.Servings = newRecipe.Servings;
            findRecipe.Image = newRecipe.Image;
            var products = _dataContext.Product.Where(x => x.FkRecipe.Equals(recipe.Id)).ToList();
            foreach (var item in products)
            {
                _dataContext.Product.Remove(item);
            }
            foreach (var item in newRecipe.Products)
            {
                var product = new Product
                {
                    Name = item.Name,
                    Quantity = item.Quantity,
                    FkRecipe = recipe.Id
                };
                await _dataContext.Product.AddAsync(product);
            }
            _dataContext.Update(findRecipe);
            await _dataContext.SaveChangesAsync();

            return true;
        }
    }
}
