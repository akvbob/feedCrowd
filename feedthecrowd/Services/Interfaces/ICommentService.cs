using FeedTheCrowd.Dtos.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Services.Interfaces
{
    public interface ICommentService
    {
        Task<ICollection<AllCommentsDto>> GetAll();
        Task<CommentDto> Create(NewCommentDto newItem);
        Task<ICollection<AllCommentsDto>> GetByRecipeId(int id);
        Task<string> Delete(int id);
        Task<string> DeleteByRecipeId(int id);
        Task<string> DeleteByUserId(int id);
        Task Update(int id, CommentDto comment);
        Task<CommentDto> GetById(int id);
    }
}
