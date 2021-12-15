using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Areas.Manager.Models
{
    public class TransactionVM
	{
		public int TransactionID { get; set; }

		[Required(ErrorMessage = "Please enter a transaction name.")]
		[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
		public string Name { get; set; }

		[Required]
		[DataType(DataType.Date)]
		//[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
		public DateTime Date { get; set; }

		[Required]
		[DisplayName("Reference")]
		public string ReferenceNo { get; set; }

		[Required]
		[DisplayName("Transaction Type")]
		[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
		public string TransactionType { get; set; }

		public int CentreNo { get; set; }
		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.00!")]
		public double Amount { get; set; }

		public string Description { get; set; }

	}
}
