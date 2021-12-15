using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DigeraitMIS.Areas.Manager.Models
{
    public class TeacherVM
    {
        [Key]
        public int UserID { get; set; }
        public int CentreNo { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Identity Number is required")]
        [DisplayName("Identity Number")]
        public string IDNumber { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Plaese enter your email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please confirm email address")]
        [Compare("EmailAddress")]
        [DisplayName("Confirm Email Address")]
        public string ConfirmEmailAddress { get; set; }

        [DisplayName("User Type")]
        public string UserType { get; set; }
        public string Password { get; set; }

        [DisplayName("User Status")]
        public string Status { get; set; }
    }
}
