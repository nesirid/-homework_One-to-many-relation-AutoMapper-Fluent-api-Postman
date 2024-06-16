using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Category : BaseEntity
    {
        [StringLength(30)]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
