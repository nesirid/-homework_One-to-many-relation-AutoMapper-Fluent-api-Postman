using WebApp.Models;

namespace WebApp.Services.Interfaces
{
    public interface ICategoryService : IService<Category>
    {
        Task<Category> CreateCategoryWithProductsAsync(Category category);
        Task<Category> UpdateCategoryWithProductsAsync(int id, Category category);
        Task<Category> GetCategoryWithProductsAsync(int id);
    }
}
