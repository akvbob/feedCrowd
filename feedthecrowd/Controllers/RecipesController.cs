using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedTheCrowd.Dtos.Products;
using FeedTheCrowd.Dtos.Recipes;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedTheCrowd.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var recipes = await _recipeService.GetAll();
            return Ok(recipes);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe([FromBody] NewRecipeDto newRecipeDto)
        {
            var createRecipe = await _recipeService.Create(newRecipeDto);
            if(createRecipe == null)
            {
                return BadRequest();
            }
            return Ok(createRecipe);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipe = await _recipeService.GetById(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }
        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetRecipesByEvent(int eventId)
        {
            var recipes = await _recipeService.GetRecipesByEvent(eventId);
            return Ok(recipes);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetRecipesByUser(int userId)

        {
            var recipes = await _recipeService.GetRecipesByUser(userId);
            return Ok(recipes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            RecipeDto comm = new RecipeDto();
            if (id > 0)
            {
                comm = await _recipeService.GetById(id);
            }
            if (id <= 0 || comm == null)
            {
                return NotFound();
            }
            var success = await _recipeService.Delete(id);

            return Ok("Recipe id:" + success + " deleted successfully");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NewRecipeDto newRecipe)
        {
            if(id <= 0)
            {
                return NotFound();
            }
            await _recipeService.Update(id, newRecipe);
            return NoContent();
        }

    }
}