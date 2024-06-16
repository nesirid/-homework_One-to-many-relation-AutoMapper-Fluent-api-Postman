using WebApp.DTOs.Products;

namespace WebApp.DTOs.Archives
{
    public class ArchiveCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ArchiveProductDto> Products { get; set; }
    }
}
