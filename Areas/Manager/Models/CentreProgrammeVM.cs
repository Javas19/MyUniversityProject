using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Areas.Manager.Models
{
    public class CentreProgrammeVM
    {
        public int CentreProgrammeID { get; set; }
        public int ProgrammeID { get; set; }
        public int CentreNo { get; set; }
        public int TeacherID { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
