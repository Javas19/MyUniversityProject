using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DigeraitMIS.Data;
using System.Data;


namespace DigeraitMIS.Controllers.Regional.Regional.Models
{
    public class ManageEcdViewModel
    {
        [Key]
        public int? CentreNo { get; set; }
        public string Name { get; set; }
        public int RegionID { get; set; }
        public int ManagerID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int RegistrationNo { get; set; }
        public string Status { get; set; }

        public Region Regions { get; set; }
        //public Manager Managers { get; set; }

        //public List<Transaction> Transactions { get; set; }
    }
}
