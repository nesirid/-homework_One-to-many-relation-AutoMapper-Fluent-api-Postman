using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs.Blogs
{
    public class BlogCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
