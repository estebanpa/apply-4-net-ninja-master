using ninja.model.Entity;
using ninja.model.Manager;
using ninja.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ninja.Controllers
{
    public class InvoiceController : Controller
    {
		private InvoiceManager _invoiceMagager;

		public InvoiceController() {
			_invoiceMagager = new InvoiceManager();
		}

        // GET: Invoice
        public ActionResult Index()
        {
			var invoices = _invoiceMagager.GetAll();
			var viewModels = new List<InvoiceViewModel>();

			AutoMapper.Mapper.Map(invoices, viewModels);

            return View(viewModels);
        }

        // GET: Invoice/Details/5
        public ActionResult Details(long id)
        {
			var invoice = _invoiceMagager.GetById(id);
			var viewModel = new InvoiceViewModel();

			AutoMapper.Mapper.Map(invoice, viewModel );

			return View(viewModel);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create(InvoiceViewModel viewModel)
        {
			if (!ModelState.IsValid)
			{
				return View();
			}

			if (_invoiceMagager.Exists(viewModel.Id))
			{
				ModelState.AddModelError(string.Empty, "Invoice already exists");
				return View();
			}

			try
            {
				var invoice = new Invoice();
				
				AutoMapper.Mapper.Map(viewModel, invoice);
				_invoiceMagager.Insert(invoice);

				return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(long id)
        {
			var invoice = _invoiceMagager.GetById(id);
			var viewModel = new InvoiceViewModel();

			AutoMapper.Mapper.Map(invoice, viewModel);

			return View(viewModel);
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(InvoiceViewModel viewModel)
        {
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
            {
				var invoice = _invoiceMagager.GetById(viewModel.Id);
				AutoMapper.Mapper.Map(viewModel, invoice);

				return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(long id)
        {
			var invoice = _invoiceMagager.GetById(id);
			var viewModel = new InvoiceViewModel();

			AutoMapper.Mapper.Map(invoice, viewModel);

			return View(viewModel);
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        public ActionResult Delete(long id, FormCollection collection)
        {
			try
            {
				_invoiceMagager.Delete(id);

				return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
