using FeedTheCrowd.Dtos.Comments;
using FeedTheCrowd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Data.Interfaces
{
    public interface ICommentRepository
    {
        Task<ICollection<Comment>> GetAll();
        Task<Comment> Add(Comment comment);
        Task<ICollection<Comment>> GetByRecipeId(int id);
        Task<string> Delete(int id);
        Task<string> DeleteByRecipeId(int id);
        Task<string> DeleteByUserId(int id);
        Task<Comment> GetById(int id);
        Task<bool> Update(Comment comment, CommentDto newComment);
    }
}
