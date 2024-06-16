using AutoMapper;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IWebHostEnvironment _environment;

        public ProductService(AppDbContext context,
                              IMapper mapper,
                              IWebHostEnvironment environment)
                                                : base(context, mapper)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }
        public async Task<string> SaveImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0) return null;

            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                throw new InvalidOperationException("SelectPath(FileLocation)");
            }

            var uploadsFolder = Path.Combine(webRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return "/uploads/" + uniqueFileName;
        }
        public async Task<Product> UpdateAsync(int id, Product entity)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return null;

            existingProduct.Name = entity.Name;
            existingProduct.Price = entity.Price;
            existingProduct.Description = entity.Description;
            if (!string.IsNullOrEmpty(entity.ImagePath))
            {
                existingProduct.ImagePath = entity.ImagePath;
            }
            existingProduct.CategoryId = entity.CategoryId;

            await _context.SaveChangesAsync();

            return existingProduct;
        }
    }
}
