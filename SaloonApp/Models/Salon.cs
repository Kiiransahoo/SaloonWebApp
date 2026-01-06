using System.ComponentModel.DataAnnotations;

namespace SaloonApp.Models
{
    public class Salon
    {
        //public int SalonId { get; set; }
        //public string Name { get; set; }
        //public string Address { get; set; }
        //public string PinCode { get; set; }
        //public string Phone { get; set; }

        public int SalonId { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Salon Type")]
        public string SalonType { get; set; }   // NEW

        [Required]
        [Display(Name = "Contact No")]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "PIN Code")]
        [StringLength(6, MinimumLength = 3)]
        public string PinCode { get; set; }


    }
}
