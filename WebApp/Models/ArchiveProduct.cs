using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ArchiveProduct : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Required]
        public string ImagePath { get; set; }
        public int ArchiveCategoryId { get; set; }
        public ArchiveCategory ArchiveCategory { get; set; }
    }
}
