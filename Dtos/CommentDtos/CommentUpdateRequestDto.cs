using System.ComponentModel.DataAnnotations;

namespace api.Dtos.CommentDtos
{
    public class CommentUpdateRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title has to be atleast 5 characters long")]
        [MaxLength(280, ErrorMessage = "Title cannot exceed 280 characters")]
        public string Title { get; set; } = String.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content has to be atleast 5 characters long")]
        [MaxLength(280, ErrorMessage = "Content cannot exceed 280 characters")]
        public string Content { get; set; } = String.Empty;
    }
}