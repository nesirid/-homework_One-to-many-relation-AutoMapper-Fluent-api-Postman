using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class BlogService : Service<Blog>, IBlogService
    {
        public BlogService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs.AsNoTracking().ToListAsync();
        }
        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Blog> CreateAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return blog;
        }
        public async Task<Blog> UpdateAsync(int id, Blog blog)
        {
            var existingBlog = await _context.Blogs.FindAsync(id);
            if (existingBlog == null) return null;

            _mapper.Map(blog, existingBlog);
            await _context.SaveChangesAsync();
            return existingBlog;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return false;

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ExistAsync(string title, string description)
        {
            return await _context.Blogs.AnyAsync(m => m.Title == title && m.Description == description);
        }
        public async Task<bool> ExistExceptByIdAsync(int id, string title, string description)
        {
            return await _context.Blogs.AnyAsync(m => m.Id != id && (m.Title == title || m.Description == description));
        }
    }
}
