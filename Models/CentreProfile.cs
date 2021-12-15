using DigeraitMIS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Models
{
    public class CentreProfile
    {
        [Key]
        public int CentreNo { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Region Name")]
        public int RegionID { get; set; }

        [DisplayName("Manager Name")]
        public int ManagerID { get; set; }
        
        [Required]
        [DisplayName("Street Address")]
        public string AddressLine1 { get; set; }

        [Required]
        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [DisplayName("Registration Number")]
        public int RegistrationNo { get; set; }

        public string Status { get; set; }


        [ForeignKey("RegionID")]
        public Region Regions { get; set; }

        [ForeignKey("ManagerID")]
        public Manager Managers { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

    }
}
