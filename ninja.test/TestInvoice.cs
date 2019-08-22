using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ninja.model.Entity;
using ninja.model.Manager;

namespace ninja.test {

    [TestClass]
    public class TestInvoice {

        [TestMethod]
        public void InsertNewInvoice() {

            InvoiceManager manager = new InvoiceManager();
            long id = 1007;
            Invoice invoice = new Invoice() {
                Id = id,
                Type = Types.A.ToString()
            };

            manager.Insert(invoice);
            Invoice result = manager.GetById(id);

            Assert.AreEqual(invoice, result);
        }

        [TestMethod]
        public void InsertNewDetailInvoice() {

			//ARRANGE
			InvoiceManager manager = new InvoiceManager();
            long id = 1006;
            var invoice = new Invoice() {
                Id = id,
                Type = Types.A.ToString()
            };

			manager.Insert(invoice);
			var detailsBeforeInsert = invoice.GetDetail().ToList();

			//ACT
			var detailToInsert = new InvoiceDetail() {
                Id = id,
                Invoice = invoice,
                Description = "Venta insumos varios",
                Amount = 14,
                UnitPrice = 4.33
            };

			var invoiceFromManager = manager.GetById(id);
			invoiceFromManager.AddDetail(detailToInsert);

			var detailInserted = invoiceFromManager.GetDetail().FirstOrDefault();

			//ASSERT
			Assert.AreNotEqual(detailsBeforeInsert.Count(), invoiceFromManager.GetDetail().Count());
			Assert.AreEqual(detailToInsert, detailInserted);
		}

        [TestMethod]
        public void DeleteInvoice() {

			/*
              1- Eliminar la factura con id=4
              2- Comprobar de que la factura con id=4 ya no exista
              3- La prueba tiene que mostrarse que se ejecuto correctamente
            */

			#region Escribir el código dentro de este bloque

			//ARRANGE
			InvoiceManager manager = new InvoiceManager();
			var id = 3;
			Invoice invoice3 = new Invoice()
			{
				Id = id,
				Type = Types.A.ToString()
			};

			invoice3.AddDetail(new InvoiceDetail()
			{
				Id = id,
				Invoice = invoice3,
				Description = "Venta insumos varios",
				Amount = 14,
				UnitPrice = 4.33
			});

			manager.Insert(invoice3);

			id = 4;
			Invoice invoice4 = new Invoice()
			{
				Id = id,
				Type = Types.B.ToString()
			};

			invoice4.AddDetail(new InvoiceDetail()
			{
				Id = id,
				Invoice = invoice4,
				Description = "Venta insumos varios",
				Amount = 14,
				UnitPrice = 4.33
			});

			id = 5;
			Invoice invoice5 = new Invoice()
			{
				Id = id,
				Type = Types.C.ToString()
			};

			invoice4.AddDetail(new InvoiceDetail()
			{
				Id = id,
				Invoice = invoice5,
				Description = "Venta insumos varios",
				Amount = 14,
				UnitPrice = 4.33
			});

			manager.Insert(invoice5);

			//ACT
			manager.Delete(invoice4.Id);

			//ASSERT
			Assert.AreEqual(invoice3, manager.GetById(invoice3.Id));
			Assert.AreEqual(null, manager.GetById(invoice4.Id));
			Assert.AreEqual(invoice5, manager.GetById(invoice5.Id));

			#endregion Escribir el código dentro de este bloque

		}

		[TestMethod]
        public void UpdateInvoiceDetail() {


			//ARRANGE
			InvoiceManager manager = new InvoiceManager();

			long id = 1003;
			Invoice invoice = new Invoice()
			{
				Id = id,
				Type = Types.A.ToString()
			};

			var detail1 = new InvoiceDetail()
			{
				Id = 1,
				Invoice = invoice,
				Description = "Venta insumos varios",
				Amount = 14,
				UnitPrice = 4.33
			};

			invoice.AddDetail(detail1);

			manager.Insert(invoice);


			//ACT
			IList<InvoiceDetail> detail = new List<InvoiceDetail>();

			var detail2 = new InvoiceDetail()
			{
				Id = 2,
				Invoice = invoice,
				Description = "Venta insumos tóner",
				Amount = 5,
				UnitPrice = 87
			};

			var detail3 = new InvoiceDetail()
			{
				Id = 3,
				Invoice = invoice,
				Description = "Venta insumos tóner",
				Amount = 5,
				UnitPrice = 87
			};

			detail.Add(detail2);
			detail.Add(detail3);

			manager.UpdateDetail(id, detail);
			Invoice result = manager.GetById(id);

			//ASSERT
			Assert.AreEqual(false, result.GetDetail().Contains(detail1));
			Assert.AreEqual(true, result.GetDetail().Contains(detail2));
			Assert.AreEqual(true, result.GetDetail().Contains(detail3));
			CollectionAssert.AreEqual(result.GetDetail().ToList(), detail.ToList());

		}

        [TestMethod]
        public void CalculateInvoiceTotalPriceWithTaxes() {

			//ARRANGE
            InvoiceManager manager = new InvoiceManager();

			//ACT
			long id = 1005;
			Invoice invoice = manager.GetById(id);

            double sum = 0;
            foreach(InvoiceDetail item in invoice.GetDetail()) 
                sum += item.TotalPrice * item.Taxes;

			//ASSERT
			Assert.AreEqual(sum, invoice.CalculateInvoiceTotalPriceWithTaxes());
        }

    }

}