using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Models
{
	public class Transaction
	{
		[Key]
		public int TransactionID { get; set; }

		[Required(ErrorMessage ="Please enter a transaction name.")]
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

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.00!")]
		public double Amount { get; set; }

		[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
		public string Description { get; set; }

		[Required]
		[DisplayName("Centre Name")]
		public int? CentreNo { get; set; }
		[ForeignKey("CentreNo")]
		public CentreProfile CentreProfile { get; set; }

		public List<Income> Incomes { get; set; }
		public List<Expenditure> Expenditures { get; set; }


		// public CentreProfile CentreProfile { get; set; }
		//public TblCentre TblCentre { get; set; }
	}
}
