using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Areas.Manager.Models
{
    public class EditTeacherDetailsVM
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string UserType { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
