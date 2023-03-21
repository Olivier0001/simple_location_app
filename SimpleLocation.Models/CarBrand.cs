using System.ComponentModel.DataAnnotations;

namespace SimpleLocation.Models
{
    public class CarBrand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
