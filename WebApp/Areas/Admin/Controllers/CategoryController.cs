using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.DTOs.Categories;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IArchiveService _archiveService;
        public CategoryController(AppDbContext context, IMapper mapper, IArchiveService archiveService)
        {
            _context = context;
            _mapper = mapper;
            _archiveService = archiveService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            var mappedDatas = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(mappedDatas);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = _mapper.Map<Category>(request);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), new { id = category.Id }, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CategoryEditDto request)
        {
            var entity = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null) return NotFound();

            _mapper.Map(request, entity);
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();

            return Ok(entity);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var entity = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null) return NotFound();

            return Ok(_mapper.Map<CategoryDto>(entity));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            var archiveCategory = _mapper.Map<ArchiveCategory>(entity);
            await _archiveService.CreateArchiveCategoryAsync(archiveCategory);

            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var categories = await _context.Categories.AsNoTracking()
                                                      .Where(c => c.Name.Contains(name))
                                                      .ToListAsync();
            var mappedDatas = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(mappedDatas);
        }
    }
}