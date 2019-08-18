using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ninja.model.Entity {

    public class InvoiceDetail {

		public long Id { get; set; }

		/// <summary>
		/// Valor del IVA para todo el dominio
		/// </summary>
		public double Taxes { get { return 1.21; } }

        public virtual Invoice Invoice { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get { return this.Amount * this.UnitPrice; } }
        public double TotalPriceWithTaxes { get { return this.TotalPrice * this.Taxes; } }
    }
}