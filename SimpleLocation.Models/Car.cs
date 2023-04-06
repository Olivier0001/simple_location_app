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
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [Range(1, 1000, ErrorMessage = "Display order must be in range of $1-$1000 !!!")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "float(18, 2)")]
        public float Price { get; set; }
        [Display(Name = "CarBrand")]
        public int CarBrandId { get; set; }
        [ForeignKey("CarBrandId")]
        public CarBrand CarBrand { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string CarStatus { get; set; }
    }
}
