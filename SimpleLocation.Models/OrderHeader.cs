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

        [Required]
        //[DisplayFormat(DataFormatString = "{0:t}")]
        [Display(Name = "Pick Up Time")]
        //[DataType(DataType.Time)]
        public DateTime PickUpTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PickUpDate { get; set; }

        [Required]
        [Display(Name = "Pick Time Of Return")]
        public DateTime PickTimeOfReturn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PickDateOfReturn { get; set; }

        public string Status { get; set; }

        public string? Comments { get; set; }

        public string? TransactionId { get; set; }

        [Display(Name = "PickupName")]
        [Required]
        public string PickupName { get; set; }


        [Display(Name = "PhoneNumber")]
        [Required]
        public string PhoneNumber { get; set; }

        public string OrderHeaderStatus { get; set; }
    }
}
