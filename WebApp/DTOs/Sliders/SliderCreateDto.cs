using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs.Sliders
{
    public class SliderCreateDto
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
