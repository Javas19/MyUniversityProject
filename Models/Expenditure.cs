using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Models
{
    public class Expenditure
    {
        [ForeignKey("TransactionID")]
        public int ExpenditureID { get; set; }

        public Transaction Transactions { get; set; }
    }
}
