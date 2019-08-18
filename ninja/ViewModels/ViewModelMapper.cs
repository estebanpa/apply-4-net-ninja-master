using ninja.model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ninja.ViewModels
{
	public class ViewModelMapper: AutoMapper.Profile
	{
		public ViewModelMapper() {
			CreateMap<Invoice, InvoiceViewModel>()
				.ForMember(m => m.InvoiceTotalPriceWithTaxes, mv => mv.MapFrom(m => m.CalculateInvoiceTotalPriceWithTaxes()));

			CreateMap<InvoiceViewModel, Invoice>();

			CreateMap<InvoiceDetail, InvoiceDetailViewModel>();

			CreateMap<InvoiceDetailViewModel, InvoiceDetail>();

		}

		public static void Run() {
			AutoMapper.Mapper.Initialize(x =>
			{
				x.AddProfile<ViewModelMapper>();
			});
		}
		
	}
}