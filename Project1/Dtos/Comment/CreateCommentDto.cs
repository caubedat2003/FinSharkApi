using System.ComponentModel.DataAnnotations;

namespace Project1.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Title must be 1 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(1, ErrorMessage = "Content must be 1 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
