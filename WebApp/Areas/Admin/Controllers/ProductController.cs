using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTOs.Products;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            var mappedDatas = _mapper.Map<List<ProductDto>>(products);
            return Ok(mappedDatas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(request);

            if (request.Image != null)
            {
                product.ImagePath = await _productService.SaveImageAsync(request.Image);
            }

            await _productService.CreateAsync(product);

            return CreatedAtAction(nameof(Create), request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductEditDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(request);

            if (request.Image != null)
            {
                product.ImagePath = await _productService.SaveImageAsync(request.Image);
            }

            var updatedProduct = await _productService.UpdateAsync(id, product);
            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);
            if (!result) return NotFound();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            return Ok(_mapper.Map<ProductDto>(product));
        }
    }
}
