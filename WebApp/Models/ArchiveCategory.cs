using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ArchiveCategory : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public ICollection<ArchiveProduct> Products { get; set; }
    }
}
