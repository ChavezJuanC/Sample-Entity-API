using api.Dtos.CommentDtos;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto CommentToDto(this CommentModel Comment)
        {
            return new CommentDto
            {
                Id = Comment.Id,
                Title = Comment.Title,
                Content = Comment.Content,
                StockId = Comment.StockId
            };
        }

        public static CommentModel DtoToComment(this CreateCommentRequestDto Comment)
        {
            return new CommentModel
            {
                Title = Comment.Title,
                Content = Comment.Content,
                StockId = Comment.StockId
            };
        }
    }
}