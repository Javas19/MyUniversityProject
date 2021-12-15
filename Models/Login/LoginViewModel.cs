using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigeraitMIS.Models.Login
{
    public class LoginViewModel
    {
        public int userID { get; set; }
        [Required(ErrorMessage ="Please enter email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter password.")]
        public string Password { get; set; }
    }
}
