using WebApp.Models;

namespace WebApp.Services.Interfaces
{
    public interface ISliderService : IService<Slider>
    {
        Task<IEnumerable<Slider>> GetAllSlidersAsync();
        Task<Slider> GetSliderByIdAsync(int id);
        Task<Slider> CreateSliderAsync(Slider slider);
        Task<Slider> UpdateSliderAsync(int id, Slider slider);
        Task<bool> DeleteSliderAsync(int id);
    }
}
