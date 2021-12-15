using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigeraitMIS.Controllers.Regional.Regional.Models
{
    public class HomePageModel
    {

        public int userID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}
