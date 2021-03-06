using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigeraitMIS.Models
{
    public class TeacherProfile
    {

        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Incorrect email.")]
        [Compare("ConfirmEmail")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Confirm email.")]
        [Display(Name = "Confirm email.")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password.")]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
