using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs.Blogs
{
    public class BlogEditDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        public IFormFile NewImage { get; set; }
        public string? ExistingImage { get; set; }
    }
}
