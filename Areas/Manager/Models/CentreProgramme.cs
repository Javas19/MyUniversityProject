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
    public class CentreProgramme
    {
        public int CentreProgrammeID { get; set; }

        [DisplayName("Programme")]
        [Required]
        public int ProgrammeID { get; set; }

        [DisplayName("Teacher")]
        [Required]
        public int TeacherID { get; set; }

        public int CentreNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Status { get; set; }

    }
}
