namespace api.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public DateTime CreatedOn { get; set; }
        public int? StockId { get; set; }
        public StockModel? Stock { get; set; }
    }
}