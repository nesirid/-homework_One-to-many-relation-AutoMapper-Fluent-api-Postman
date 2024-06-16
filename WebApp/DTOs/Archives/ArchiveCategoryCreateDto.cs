using System.ComponentModel.DataAnnotations;
using WebApp.DTOs.Products;

namespace WebApp.DTOs.Archives
{
    public class ArchiveCategoryCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
