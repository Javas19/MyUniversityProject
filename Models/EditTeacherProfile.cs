using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigeraitMIS.Models
{
    public class EditTeacherProfile
    {
        [Required(ErrorMessage = "Plaese enter your email address")]

        public string emailAddress { get; set; }
        [Required(ErrorMessage = "Please confirm email address")]
        [Compare("emailAddress")]
        [Display(Name = "Confirm Email Address")]
        public string ConfirmEmailAddress { get; set; }


    }
}
