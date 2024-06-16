﻿namespace WebApp.DTOs.Products
{
    public class ProductEditDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
