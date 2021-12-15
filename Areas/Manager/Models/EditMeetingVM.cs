using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Areas.Manager.Models
{
    public class EditMeetingVM
    {
        public int MeetingID { get; set; }
        public string Date { get; set; }

        [Required]
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
