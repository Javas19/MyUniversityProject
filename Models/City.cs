using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DigeraitMIS.Data;

namespace DigeraitMIS.Models
{
    public class City
    {
        [Key]
        public int CityID { get; set; }
        public int ProvinceID { get; set; }
        public string CityName { get; set; }
        public string Status { get; set; }

        public Province Province { get; set; }
        public List<Region> Regions { get; set; }
    }
}
