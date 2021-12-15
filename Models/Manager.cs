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
    public class Manager
    {
        [Key]
        public int ManagerID { get; set; }

        [ForeignKey("ManagerID")]

        [DisplayName("Identity Number")]
        public int IDNumber { get; set; }
        public User Users { get; set; }

        public ICollection<CentreProfile> CentreProfile { get; set; }
    }
}
