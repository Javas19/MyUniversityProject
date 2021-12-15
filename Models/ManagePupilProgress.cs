using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigeraitMIS.Models
{
    public class ManagePupilProgress
    {   [Required(ErrorMessage ="Please select a program")]
        public int ProgrammeID { get; set; }
        [Required(ErrorMessage ="Please select a pupil")]
        public int PupilID { get; set; }
        [Required(ErrorMessage ="Please select absent or present")]
        public int RegsiterID { get; set; }
        [Required(ErrorMessage ="Please enter ratings")]
        public double Ratings { get; set; }
        [Required(ErrorMessage ="Please enter date")]
        public string Date { get; set; }

        [Required(ErrorMessage ="Please select Term")]
        public string Term { get; set; }

        public string Status { get; set; }
    }
}
