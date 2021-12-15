using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigeraitMIS.Models
{
    public class TermReportVM
    {
        [Required(ErrorMessage ="Please select programme.")]
        public int ProgrammeId { get; set; }

        [Required(ErrorMessage ="Please select pupil.")]
        public int PupilID { get; set; }
        [Required(ErrorMessage ="Please select Term.")]
        public string Term { get; set; }

        public int AbsentDays { get; set; }
        public string OverallRating { get; set; }

        [Required(ErrorMessage ="Please type your comments.")]
        public string Comments { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }

    }
}
