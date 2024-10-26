using api.Dtos.CommentDtos;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentModel>> GetAllAsync();
        Task<CommentModel?> GetCommentByIdAsync(int id);
    }
}