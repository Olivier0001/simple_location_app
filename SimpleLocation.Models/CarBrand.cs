using System.ComponentModel.DataAnnotations;

namespace SimpleLocation.Models
{
    public class CarBrand
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Le champ Marque est obligatoire")]
        public string Name { get; set; }

    }
}
