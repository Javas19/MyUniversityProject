using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DigeraitMIS.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        public string Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("User Type")]
        public string UserType { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Status")]
        public string UserStatus { get; set; }

        [DisplayName("Full Name")]
        public string FullName
        {
            get{ return Surname + ", " + FirstName; }
        }

        public ICollection<Manager> Managers { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}
