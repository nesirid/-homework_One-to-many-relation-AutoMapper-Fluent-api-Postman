using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.DTOs.Countries;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CountryController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CountryController(AppDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var countries = await _context.Countries.AsNoTracking().ToListAsync();

            var mappedDatas = _mapper.Map<List<CountryDto>>(countries);

            return Ok(mappedDatas);

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CountryCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _context.Countries.AddAsync(_mapper.Map<Country>(request));

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CountryEditDto request)
        {
            var entity = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(m=>m.Id == id);
            if (entity == null) return NotFound();

            _mapper.Map(request, entity);

            _context.Countries.Update(entity);
            await _context.SaveChangesAsync();

            return Ok(entity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var entity = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (entity == null) return NotFound();

            return Ok(_mapper.Map<CountryDto>(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
