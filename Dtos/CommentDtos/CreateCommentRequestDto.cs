namespace api.Dtos.CommentDtos
{
    public class CreateCommentRequestDto
    {
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public int StockId { get; set; }
    }
}