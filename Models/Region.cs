using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DigeraitMIS.Models;

namespace DigeraitMIS.Data
{
    public class Region
    {
        [Key]
        public int RegionID { get; set; }
        public int CityID { get; set; }
        public string RegionName { get; set; }
        public string Status { get; set; }

        public City City { get; set; }
        public List<CentreProfile> CentreProfiles { get; set; }
    }
}
