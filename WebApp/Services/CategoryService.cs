using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public async Task<Category> CreateCategoryWithProductsAsync(Category category)
        {
            foreach (var product in category.Products)
            {
                product.Category = category;
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<Category> UpdateCategoryWithProductsAsync(int id, Category category)
        {
            var existingCategory = await _context.Categories.Include(c => c.Products)
                                                             .FirstOrDefaultAsync(c => c.Id == id);
            if (existingCategory == null)
                return null;

            existingCategory.Name = category.Name;

            _context.Products.RemoveRange(existingCategory.Products);
            existingCategory.Products = category.Products;

            foreach (var product in existingCategory.Products)
            {
                product.Category = existingCategory;
            }

            await _context.SaveChangesAsync();
            return existingCategory;
        }
        public async Task<Category> GetCategoryWithProductsAsync(int id)
        {
            return await _context.Categories.Include(c => c.Products)
                                            .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}