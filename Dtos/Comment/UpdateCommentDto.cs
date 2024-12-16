using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Author name must be at least 3 characters long")]
        public string Author { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 10 characters long")]
        [MaxLength(50, ErrorMessage = "Title must be at most 50 characters long")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Body must be at least 10 characters long")]
        [MaxLength(500, ErrorMessage = "Body must be at most 250 characters long")]
        public string Body { get; set; } = string.Empty;
    }
}