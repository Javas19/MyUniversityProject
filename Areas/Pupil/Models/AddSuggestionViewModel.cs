using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigeraitMIS.Areas.Pupil.Models
{
    public class AddSuggestionViewModel
    {
        [Required(ErrorMessage ="Please enter suggestion.")]
        public string Suggestion { get; set; }

        public int PupilId { get; set; }
        public string date { get; set; }
        public string status { get; set; }
    }
}
