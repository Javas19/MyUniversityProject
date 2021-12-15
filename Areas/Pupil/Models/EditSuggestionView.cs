using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigeraitMIS.Areas.Pupil.Models
{
    public class EditSuggestionView
    {
        public int SuggestionId { get; set; }


        public string Suggestion { get; set; }
    }
}
