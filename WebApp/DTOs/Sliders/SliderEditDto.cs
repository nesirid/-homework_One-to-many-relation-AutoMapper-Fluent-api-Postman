using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs.Sliders
{
    public class SliderEditDto
    {
        public IFormFile NewImage { get; set; }
        public string? ExistingImage { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
