using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigeraitMIS.Models
{
    public class Pupil
    {
        [Key]
        public int PupilID { get; set; }

        [Required(ErrorMessage ="Please Enter your name.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage ="Please enter your surname.")]
        public string Surname { get; set; }


        [Required(ErrorMessage ="Please enter Email.")]
        [EmailAddress(ErrorMessage ="Incorrect email.")]
        [Compare("ConfirmEmail")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage ="Confirm Email.")]
        [EmailAddress(ErrorMessage ="Incorrect email.")]
        [Display(Name ="Confirm Email")]
        public string ConfirmEmail { get; set; }

        public string UserType { get; set; }

        [Required(ErrorMessage ="Enter password.")]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }


        [Required(ErrorMessage ="Confirm Password")]
        [Display(Name ="Confirm Passoword")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Please enter guardian name.")]
        public string GuardianName { get; set; }


        [Required(ErrorMessage ="Please enter your ID number")]
        public string IDnumber { get; set; }


        public string DateOfBirth { get; set; }


        [Required(ErrorMessage ="please enter address.")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Please choose a centre")]
        public int CentreNo { get; set; }

        [Required(ErrorMessage ="Please select a region.")]
        public int RegionID { get; set; }
        [Required(ErrorMessage ="Please select a province")]
        public int ProvinceID { get; set; }
        
        [Required(ErrorMessage ="Please select a city")]
        public int CityID { get; set; }
        public string UserStatus { get; set; }
    }
}
