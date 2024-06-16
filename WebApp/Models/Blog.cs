using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Blog : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
