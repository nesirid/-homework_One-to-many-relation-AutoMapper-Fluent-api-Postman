using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.DTOs.Blogs;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.Admin.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService, IWebHostEnvironment env, IMapper mapper)
        {
            _blogService = blogService;
            _env = env;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await _blogService.GetAllAsync();
            var mappedBlogs = _mapper.Map<List<BlogDto>>(blogs);
            return Ok(mappedBlogs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BlogCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;
            var folderPath = Path.Combine(_env.WebRootPath, "img");
            var filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            var blog = new Blog
            {
                Title = request.Title,
                Description = request.Description,
                Image = fileName
            };

            var createdBlog = await _blogService.CreateAsync(blog);

            return CreatedAtAction(nameof(Create), new { id = createdBlog.Id }, createdBlog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] BlogEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingBlog = await _blogService.GetByIdAsync(id);
            if (existingBlog == null) return NotFound();

            if (request.NewImage != null)
            {
                var oldFilePath = Path.Combine(_env.WebRootPath, "img", existingBlog.Image);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                var fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                var filePath = Path.Combine(_env.WebRootPath, "img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.NewImage.CopyToAsync(stream);
                }

                existingBlog.Image = fileName;
            }

            existingBlog.Title = request.Title;
            existingBlog.Description = request.Description;

            var updatedBlog = await _blogService.UpdateAsync(id, existingBlog);

            return Ok(updatedBlog);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null) return NotFound();

            var mappedBlog = _mapper.Map<BlogDto>(blog);
            return Ok(mappedBlog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _blogService.DeleteAsync(id);
            if (!result) return NotFound();

            return Ok();
        }
    }
}
