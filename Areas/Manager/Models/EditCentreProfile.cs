using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DigeraitMIS.Areas.Manager.Models
{
    public class EditCentreProfile
    {
        public int? CentreNo { get; set; }
        public string Name { get; set; }
        public int RegionID { get; set; }
        public int ManagerId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string RegistrationNo { get; set; }
        

        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Incorrect email.")]
        [Compare("ConfirmEmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Confirm email.")]
        [Display(Name = "Confirm email.")]
        public string ConfirmEmail { get; set; }

    }
}
