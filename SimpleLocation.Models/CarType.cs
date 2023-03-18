using System.ComponentModel.DataAnnotations;

namespace SimpleLocation.Models
{
    public class CarType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
    }
}
