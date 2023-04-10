using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLocation.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Le champ Modele est obligatoire")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Le champ Description est obligatoire")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Le champ Image est obligatoire")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Le champ Prix est obligatoire")]
        [Range(1, 1000, ErrorMessage = "L'ordre d'affichage doit être compris entre 1 et 100€ !!!")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "float(18, 2)")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Le champ Marque est obligatoire")]
        [Display(Name = "CarBrand")]
        public int CarBrandId { get; set; }
        [ForeignKey("CarBrandId")]
        public CarBrand CarBrand { get; set; }
        [Required(ErrorMessage = "Le champ Catégorie est obligatoire")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string CarStatus { get; set; }
    }
}
