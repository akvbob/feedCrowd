using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.Comments;
using FeedTheCrowd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly FeedTheCrowdContext _dataContext;

        public CommentRepository(FeedTheCrowdContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Comment> Add(Comment comment)
        {
            await _dataContext.AddAsync(comment);
            await _dataContext.SaveChangesAsync();
            return comment;
        }

        public async Task<ICollection<Comment>> GetAll()
        {
            var values = await _dataContext.Comment.ToListAsync();
            return values;
        }
        public async Task<Comment> GetById(int id)
        {
            var value = await _dataContext.Comment.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return value;
        }
        public async Task<ICollection<Comment>> GetByRecipeId(int id)
        {
            var values = await _dataContext.Comment.Where(x => x.FkRecipe.Equals(id)).ToListAsync();
            foreach(var v in values)
            {
                v.FkUserNavigation = await _dataContext.User.Where(x => x.Id.Equals(v.FkUser)).FirstOrDefaultAsync();
            }
            return values;
        }
        public async Task<string> Delete(int id)
        {
            var value = await _dataContext.Comment.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if(value == null)
            {
                return null;
            }

            var values = _dataContext.Comment.Remove(value);
            await _dataContext.SaveChangesAsync();

            return id.ToString();
        }

        public async Task<string> DeleteByRecipeId(int id)
        {
            var values = await _dataContext.Comment.Where(x => x.FkRecipe.Equals(id)).ToListAsync();
            if (values.Count == 0 || values == null)
            {
                return null;
            }
            foreach (var value in values){
                _dataContext.Comment.Remove(value);
            }
            await _dataContext.SaveChangesAsync();

            return id.ToString();
        }
        public async Task<string> DeleteByUserId(int id)
        {
            var values = await _dataContext.Comment.Where(x => x.FkUser.Equals(id)).ToListAsync();
            if (values.Count == 0 || values == null)
            {
                return null;
            }
            foreach (var value in values)
            {
                _dataContext.Comment.Remove(value);
            }
            await _dataContext.SaveChangesAsync();

            return id.ToString();
        }
        public async Task<bool> Update(Comment comment, CommentDto newComment)
        {
            var findComment = await _dataContext.Comment.FirstOrDefaultAsync(x => x.Id.Equals(comment.Id));
            if (findComment == null)
            {
                throw new InvalidOperationException($"Item {comment.Id} was not found");
            }

            findComment.TextField = newComment.TextField;
            _dataContext.Update(findComment);
            await _dataContext.SaveChangesAsync();

            return true;
        }
    }
}
