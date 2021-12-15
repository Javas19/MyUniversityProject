using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Areas.Pupil.Models
{
    public class ReportViewModel
    {
        public int ReportID { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Days { get; set; }
        public string Date { get; set; }
        public string Comments { get; set; }
        public string Ratings { get; set; }
        public string Centre { get; set; }
        public string Term { get; set; }
    }
}
