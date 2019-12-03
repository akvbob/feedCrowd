using AutoMapper;
using FeedTheCrowd.Data.Interfaces;
using FeedTheCrowd.Dtos.Comments;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedTheCrowd.Services
{
    public class CommentService : ICommentService

    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CommentDto> Create(NewCommentDto newItem)
        {
            if (newItem == null)
                throw new ArgumentNullException();

            var comment = _mapper.Map<Comment>(newItem);
            comment.DateCreated = DateTime.Now;
            await _repository.Add(comment);

            var commentDto = _mapper.Map<CommentDto>(comment);
            return commentDto;
        }

        public async Task<ICollection<AllCommentsDto>> GetAll()
        {
            var comments = await _repository.GetAll();
            var commentsDtos = _mapper.Map<AllCommentsDto[]>(comments);
            return commentsDtos;
        }
        public async Task<ICollection<AllCommentsDto>> GetByRecipeId(int id)
        {
            var comments = await _repository.GetByRecipeId(id);
            var commentsDtos = comments.Select(c => _mapper.Map<AllCommentsDto>(c)).ToArray();// _mapper.Map<AllCommentsDto[]>(comments);
            
            return commentsDtos;
        }
        public async Task<string> Delete(int id)
        {
            var success = await _repository.Delete(id);
            return success;
        }
        public async Task<string> DeleteByRecipeId(int id)
        {
            var success = await _repository.DeleteByRecipeId(id);
            return success;
        }
        public async Task<string> DeleteByUserId(int id)
        {
            var success = await _repository.DeleteByUserId(id);
            return success;
        }
        public async Task Update(int id, CommentDto comment)
        {
            if(comment == null)
                throw new ArgumentNullException();

            var itemToUpdate = await _repository.GetById(id);
            if(itemToUpdate == null)
            {
                throw new InvalidOperationException($"Comment with {id} id was not found");
            }
            await _repository.Update(itemToUpdate, comment);
        }
        public async Task<CommentDto> GetById(int id)
        {
            var comm = await _repository.GetById(id);
            var commDto = _mapper.Map<CommentDto>(comm);
            return commDto;
        }
    }
}
