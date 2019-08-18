using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ninja.ViewModels
{
	public class InvoiceDetailViewModel
	{
		[DisplayName("Id")]
		[Required(ErrorMessage = "Id is required")]
		public long Id { get; set; }

		[DisplayName("Invoice Id")]
		public long InvoiceId { get; set; }

		[DisplayName("Taxes")]
		public double Taxes { get { return 1.21; } }

		[DisplayName("Description")]
		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; }

		[DisplayName("Amount")]
		[Required(ErrorMessage = "Amount is required")]
		public double Amount { get; set; }

		[DisplayName("Unit Price")]
		[Required(ErrorMessage = "Unit Price is required")]
		public double UnitPrice { get; set; }

		[DisplayName("Total Price")]
		public double TotalPrice { get; set; }

		[DisplayName("Total Price With Taxes")]
		public double TotalPriceWithTaxes { get; set; }
	}
}