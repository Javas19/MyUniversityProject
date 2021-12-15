using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Models
{
    public class Suggestions
    {
        public int SuggestionsID { get; set; }
        public int PupilID { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}
