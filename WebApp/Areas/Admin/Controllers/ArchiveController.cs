using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.DTOs.Archives;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.Admin.Controllers
{
    public class ArchiveController : BaseController
    {
        private readonly IArchiveService _archiveService;
        private readonly IMapper _mapper;

        public ArchiveController(IArchiveService archiveService, IMapper mapper)
        {
            _archiveService = archiveService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var archives = await _archiveService.GetAllArchiveCategoriesAsync();
            var mappedArchives = _mapper.Map<List<ArchiveCategoryDto>>(archives);
            return Ok(mappedArchives);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var archiveCategory = await _archiveService.GetArchiveCategoryByIdAsync(id);
            if (archiveCategory == null) return NotFound();

            var mappedCategory = _mapper.Map<ArchiveCategoryDto>(archiveCategory);
            return Ok(mappedCategory);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _archiveService.DeleteArchiveCategoryAsync(id);
            if (!result) return NotFound();

            return Ok();
        }
        [HttpPost("Restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _archiveService.RestoreCategoryAsync(id);
            if (!result) return NotFound();
            return Ok();
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var archiveCategories = await _archiveService.SearchArchiveCategoriesAsync(query);
            var mappedCategories = _mapper.Map<List<ArchiveCategoryDto>>(archiveCategories);
            return Ok(mappedCategories);
        }
    }
}