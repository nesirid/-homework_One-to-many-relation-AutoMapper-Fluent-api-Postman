using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Slider : BaseEntity
    {
        [Required]
        public string Image { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
