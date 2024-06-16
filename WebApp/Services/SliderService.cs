using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class SliderService : Service<Slider>, ISliderService
    {
        public SliderService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<IEnumerable<Slider>> GetAllSlidersAsync()
        {
            return await _context.Sliders.AsNoTracking().ToListAsync();
        }
        public async Task<Slider> GetSliderByIdAsync(int id)
        {
            return await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Slider> CreateSliderAsync(Slider slider)
        {
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return slider;
        }
        public async Task<Slider> UpdateSliderAsync(int id, Slider slider)
        {
            var existingSlider = await _context.Sliders.FindAsync(id);
            if (existingSlider == null) return null;

            _mapper.Map(slider, existingSlider);
            await _context.SaveChangesAsync();
            return existingSlider;
        }
        public async Task<bool> DeleteSliderAsync(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return false;

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
