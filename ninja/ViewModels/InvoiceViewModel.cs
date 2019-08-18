using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ninja.ViewModels
{
	public class InvoiceViewModel
	{
		[DisplayName("Id")]
		[Required(ErrorMessage = "Id is required")]
		public long Id { get; set; }

		[DisplayName("Type")]
		[Required(ErrorMessage = "Type is required")]
		public string Type { get; set; }

		[DisplayName("Total Price With Taxes")]
		public double InvoiceTotalPriceWithTaxes { get; set; }
	}
}