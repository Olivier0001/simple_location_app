using System.ComponentModel.DataAnnotations;

namespace SimpleLocation.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Le champ Catégorie est obligatoire")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Le champ Ordre est obligatoire.")]
        [Display(Name = "Display Order")]
        [Range(1, 100, ErrorMessage = " L'ordre d'affichage doit être compris entre 1 et 100 !!!")]
        public int DisplayOrder { get; set; }
        
    }
}
