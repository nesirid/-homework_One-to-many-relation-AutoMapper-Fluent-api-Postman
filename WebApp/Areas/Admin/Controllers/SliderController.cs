using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.DTOs.Sliders;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;


        public SliderController(ISliderService sliderService, IWebHostEnvironment env, IMapper mapper)
        {
            _sliderService = sliderService;
            _env = env;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sliders = await _sliderService.GetAllSlidersAsync();
            var mappedDatas = _mapper.Map<List<SliderDto>>(sliders);
            return Ok(mappedDatas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderCreateDto request)
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

            var slider = new Slider { Image = fileName, Text = request.Text };
            var createdSlider = await _sliderService.CreateSliderAsync(slider);

            return CreatedAtAction(nameof(Create), new { id = createdSlider.Id }, createdSlider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] SliderEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingSlider = await _sliderService.GetSliderByIdAsync(id);
            if (existingSlider == null) return NotFound();

            if (request.NewImage != null)
            {
                var fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                var filePath = Path.Combine(_env.WebRootPath, "img", fileName);

                if (!Directory.Exists(Path.Combine(_env.WebRootPath, "img")))
                {
                    Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "img"));
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.NewImage.CopyToAsync(stream);
                }
                var newSlider = new Slider { Image = fileName, Text = request.Text };
                await _sliderService.CreateSliderAsync(newSlider);
            }
            return Ok(existingSlider);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var slider = await _sliderService.GetSliderByIdAsync(id);
            if (slider == null) return NotFound();

            var mappedSlider = _mapper.Map<SliderDto>(slider);
            return Ok(mappedSlider);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sliderService.DeleteSliderAsync(id);
            if (!result) return NotFound();

            return Ok();
        }
    }
}
