using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class ArchiveService : IArchiveService
    {
        private readonly AppDbContext _context;

        public ArchiveService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ArchiveCategory>> GetAllArchiveCategoriesAsync()
        {
            return await _context.ArchiveCategories.Include(a => a.Products).AsNoTracking().ToListAsync();
        }
        public async Task<ArchiveCategory> GetArchiveCategoryByIdAsync(int id)
        {
            return await _context.ArchiveCategories.Include(a => a.Products).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<ArchiveCategory> CreateArchiveCategoryAsync(ArchiveCategory archiveCategory)
        {
            await _context.ArchiveCategories.AddAsync(archiveCategory);
            await _context.SaveChangesAsync();
            return archiveCategory;
        }
        public async Task<bool> DeleteArchiveCategoryAsync(int id)
        {
            var archiveCategory = await _context.ArchiveCategories.Include(a => a.Products).FirstOrDefaultAsync(a => a.Id == id);
            if (archiveCategory == null) return false;

            _context.ArchiveCategories.Remove(archiveCategory);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RestoreCategoryAsync(int id)
        {
            var archiveCategory = await _context.ArchiveCategories.Include(a => a.Products).FirstOrDefaultAsync(a => a.Id == id);
            if (archiveCategory == null) return false;

            var category = new Category
            {
                Name = archiveCategory.Name,
                Products = archiveCategory.Products.Select(m => new Product
                {
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    ImagePath = m.ImagePath
                }).ToList()
            };

            _context.Categories.Add(category);
            _context.ArchiveCategories.Remove(archiveCategory);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<ArchiveCategory>> SearchArchiveCategoriesAsync(string query)
        {
            return await _context.ArchiveCategories
                .Where(m => m.Name.Contains(query))
                .Include(m => m.Products)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
