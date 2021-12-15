using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Models.ViewModels
{
    [Keyless]
    public class CentreProfileVM
    {
        public CentreProfile CentreProfile { get; set; }
        //public int CentreNo { get; set; }
        //public string Name { get; set; }
        //public int RegionID { get; set; }
        //public int ManagerID { get; set; }
        //public string AddressLine1 { get; set; }
        //public string AddressLine2 { get; set; }
        //public int RegistrationNo { get; set; }
        //public string Status { get; set; }
        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
        public IEnumerable<SelectListItem> TypeDropDown1 { get; set; }
    }
}
