using WebApp.Models;

namespace WebApp.Services.Interfaces
{
    public interface IBlogService : IService<Blog>
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<Blog> CreateAsync(Blog blog);
        Task<Blog> UpdateAsync(int id, Blog blog);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistAsync(string title, string description);
        Task<bool> ExistExceptByIdAsync(int id, string title, string description);
    }
}
