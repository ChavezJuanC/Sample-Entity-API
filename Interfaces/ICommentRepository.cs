using api.Dtos.CommentDtos;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentModel>> GetAllAsync();
        Task<CommentModel?> GetCommentByIdAsync(int id);
        Task<CommentModel?> CreateCommentAsync(int StockId, CommentModel comment);
        Task<CommentModel?> DeleteCommentAsync(int id);
        Task<CommentModel?> UpdateCommentAsync(int id, CommentUpdateRequestDto commentDto);
    }
}