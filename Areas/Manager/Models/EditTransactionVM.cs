using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Areas.Manager.Models
{
    public class EditTransactionVM
    {
		[Key]
		public int TransactionID { get; set; }

		[Required(ErrorMessage = "Please enter a transaction name.")]
		[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
		public string Name { get; set; }

		[Required]
		[DisplayName("Transaction Type")]
		[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
		public string TransactionType { get; set; }

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.00!")]
		public double Amount { get; set; }

		public string Description { get; set; }
	}
}
