using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ninja.model.Entity;

namespace ninja.model.Mock {

    public class InvoiceMock {

        #region Singleton

        private static InvoiceMock _instance = null;

        public static InvoiceMock GetInstance() {

            if(_instance == null)
                _instance = new InvoiceMock();

            return _instance;

        }

        #endregion Singleton

        /// <summary>
        /// Mock DB
        /// </summary>
        private IList<Invoice> _db;

        private InvoiceMock() {

            this._db = new List<Invoice>();

            this.Init();

        }

        private void Init() {

            this.Insert(new Invoice() {
                Id = 1000,
                Type = Types.A.ToString()
            });

            this.Insert(new Invoice() {
                Id = 1002,
                Type = Types.B.ToString()
            });

            Invoice invoice3 = new Invoice() {
                Id = 1003, Type = Types.A.ToString()
            };

            invoice3.AddDetail(new InvoiceDetail() { Id = 1, Invoice = invoice3, Amount = 22, Description = "Venta varias", UnitPrice = 98.1 });

            this.Insert(invoice3);

            this.Insert(new Invoice() {
                Id = 1004,
                Type = Types.A.ToString()
            });


            Invoice invoice5 = new Invoice() {
                Id = 1005, Type = Types.A.ToString()
            };

            invoice5.AddDetail(new InvoiceDetail() { Id = 1, Invoice = invoice5, Amount = 22, Description = "Venta varias", UnitPrice = 98.1 });
            invoice5.AddDetail(new InvoiceDetail() { Id = 2, Invoice = invoice5, Description = "Venta insumos varios", Amount = 14, UnitPrice = 4.33 });
            invoice5.AddDetail(new InvoiceDetail() { Id = 3, Invoice = invoice5, Description = "Venta insumos tóner", Amount = 5, UnitPrice = 87 });

            this.Insert(invoice5);

        }

        public void Delete(Invoice invoice) {

            this._db.Remove(invoice);

        }

        public IList<Invoice> GetAll() {

            return this._db;

        }

        public Invoice GetById(long id) {

            return this._db.Where(x => x.Id == id).FirstOrDefault();

        }

        public void Insert(Invoice item) {
			if (!this.Exists(item.Id))
			{
				this._db.Add(item);
			}
        }

        public Boolean Exists(long id) {

            return this._db.Where(x => x.Id == id).Any();

        }

        public void DeleteDetail(long id) {

            this.GetById(id).DeleteDetails();

        }

        public void AddDetail(long id, IList<InvoiceDetail> detail) {

            foreach(InvoiceDetail item in detail)
                this.GetById(id).AddDetail(item);

        }

    }

}