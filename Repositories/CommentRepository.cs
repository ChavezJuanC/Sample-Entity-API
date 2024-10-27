using api.Data;
using api.Dtos;
using api.Dtos.CommentDtos;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<CommentModel>> GetAllAsync()
        {
            var comments = await _context.Comments.ToListAsync();

            return (comments);
        }

        public async Task<CommentModel?> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(comment => comment.Id == id);

            if (comment == null)
            {
                return null;
            }

            return comment;
        }

        public async Task<CommentModel?> CreateCommentAsync(int StockId, CommentModel comment)
        {
            var parentStock = await _context.Stocks.FindAsync(StockId);

            if (parentStock == null)
            {
                return null;
            }

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<CommentModel?> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;

        }

        public async Task<CommentModel?> UpdateCommentAsync(int id, CommentUpdateRequestDto commentDto)
        {
            var UpdateComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (UpdateComment == null)
            {
                return null;
            }

            UpdateComment.Title = commentDto.Title;
            UpdateComment.Content = commentDto.Content;

            await _context.SaveChangesAsync();

            return UpdateComment;
        }
    }
}