using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Models.ViewModels
{
    [Keyless]
    public class ManageStaffVM
    {
        public User User { get; set; }

        //public string FirstName { get; set; }
        //public string Surname { get; set; }
        //public string UserStatus { get; set; }

        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
    }
}
