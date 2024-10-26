using api.Data;
using api.Interfaces;
using api.Mappers;
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
    }
}