using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Entity {

    public class Invoice {

        public Invoice() {

            this.Detail = new List<InvoiceDetail>();

        }

        /// <summary>
        /// Numero de factura
        /// </summary>
      
        public long Id { get; set; }
        public string Type { get; set; }
        private IList<InvoiceDetail> Detail { get; set; }

        public IList<InvoiceDetail> GetDetail() {

            return this.Detail;

        }

        public void AddDetail(InvoiceDetail detail)
		{
            this.Detail.Add(detail);
        }

		public void DeleteDetail(InvoiceDetail detail)
		{
			this.Detail.Remove(detail);
		}

		public void DeleteDetails() {

            this.Detail.Clear();

        }

		public InvoiceDetail InvoiceDetailById(long id)
		{
			return GetDetail().FirstOrDefault(x => x.Id == id);
		}

        /// <summary>
        /// Sumar el TotalPrice de cada elemento del detalle
        /// </summary>
        /// <returns></returns>
        public double CalculateInvoiceTotalPriceWithTaxes() {

            double sum = 0;

            foreach(InvoiceDetail item in this.Detail)
                sum += item.TotalPriceWithTaxes;

            return sum;

        }

    }

}