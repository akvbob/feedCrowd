using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeedTheCrowd.Dtos.Comments;

namespace FeedTheCrowd.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var comments = await _commentService.GetAll();
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] NewCommentDto newCommentDto)
        {
            var createComment = await _commentService.Create(newCommentDto);
            if (createComment == null)
            {
                return BadRequest();
            }
            return Ok(createComment);
        }

        [HttpGet("{id}/recipe")]
        public async Task<IActionResult> GetByRecipeId(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var recipeComments = await _commentService.GetByRecipeId(id);

            return Ok(recipeComments);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CommentDto comm = new CommentDto();
            if (id > 0)
            {
                comm = await _commentService.GetById(id);
            }
            if (id <= 0 || comm == null)
            {
                return NotFound();
            }
            var success = await _commentService.Delete(id);

            return Ok("Comment id:" + success + " deleted successfully");
        }

        [HttpDelete("{id}/recipe")]
        public async Task<IActionResult> DeleteByRecipeId(int id)
        {
            ICollection<AllCommentsDto> comm = new List<AllCommentsDto>();
            if (id > 0)
            {
                comm = await _commentService.GetByRecipeId(id);
            }
            if (id <= 0 || comm == null)
            {
                return NotFound();
            }
            var success = await _commentService.DeleteByRecipeId(id);

            return Ok("Comments by recipe id:" + success + " deleted successfully");
            
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentDto newComment)
        {
            CommentDto comm = new CommentDto();
            if (id > 0)
            {
                comm = await _commentService.GetById(id);
            }
            if (id <= 0 || comm == null)
            {
                return NotFound();
            }
            await _commentService.Update(id, newComment);

            return NoContent();
        }
    }
}