using WebApp.Models;

namespace WebApp.Services.Interfaces
{
    public interface IProductService : IService<Product>
    {
        Task<string> SaveImageAsync(IFormFile image);
    }
}
