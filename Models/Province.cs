using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace DigeraitMIS.Models
{
    public class Province
    {
        [Key]
        public int ProvinceID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public List<City> City { get; set; }
    }
}
