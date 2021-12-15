using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Models
{
    public class Income
    {
        [ForeignKey("TransactionID")]
        public int IncomeID { get; set; }

        public Transaction Transactions { get; set; }
    }
}
