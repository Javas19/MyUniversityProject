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
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }

        [ForeignKey("TeacherID")]
        public User Users { get; set; }
    }
}
