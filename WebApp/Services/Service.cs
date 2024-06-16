using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class Service<T> : IService<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;

        public Service(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(m => EF.Property<int>(m, "Id") == id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity == null)
                return null;

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(m => EF.Property<int>(m, "Id") == id);
            if (entity == null) return false;

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
