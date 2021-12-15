using DigeraitMIS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Areas.Manager.Models
{
    public class TeacherViewModel
    {
        [Key]
        public int TeacherID { get; set; }
        public int IDNumber { get; set; }
        public string FullNames { get; set; }
        public int OfficeNumber { get; set; }
        public int CentreNo { get; set; }

    }
}
