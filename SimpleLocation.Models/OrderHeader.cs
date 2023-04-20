using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLocation.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public User User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }

        [Required(ErrorMessage = "Le champ Heure de début est obligatoire")]
        [Display(Name = "Pick Up Time")]
        public DateTime PickUpTime { get; set; }

        [Required(ErrorMessage = "Le champ Date de début est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime PickUpDate { get; set; }

        [Required(ErrorMessage = "Le champ Heure de retour est obligatoire")]
        [Display(Name = "Pick Time Of Return")]
        public DateTime PickTimeOfReturn { get; set; }

        [Required(ErrorMessage = "Le champ Date de retour est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime PickDateOfReturn { get; set; }

        public string Status { get; set; }

        public string? Comments { get; set; }

        public string? TransactionId { get; set; }

        [Display(Name = "PickupName")]
        [Required(ErrorMessage = "Le champ Nom est obligatoire")]
        public string PickupName { get; set; }


        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "Le champ Téléphone est obligatoire")]
        public string PhoneNumber { get; set; }

        public string OrderHeaderStatus { get; set; }
    }
}
