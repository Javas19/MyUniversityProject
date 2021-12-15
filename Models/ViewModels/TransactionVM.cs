using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Models.ViewModels
{
    [Keyless]
    public class TransactionVM
    {
        public Transaction Transaction { get; set; }
        //public EditTransaction EditTransaction { get; set;
        //}
        ////  public int CentreNo { get; set; }

        ////public int TransactionID { get; set; }
        ////public string Name { get; set; }
        ////public DateTime Date { get; set; }
        ////public string ReferenceNo { get; set; }
        //public string TransactionType { get; set; }
        //public double Amount { get; set; }
        //public string Description { get; set; }
        //public int CentreNo { get; set; }
        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
    }
}
