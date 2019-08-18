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
    public class InvoiceDetailController : Controller
    {
		private InvoiceManager _invoiceMagager;

		public InvoiceDetailController() {
			_invoiceMagager = new InvoiceManager();
		}

		// GET: InvoiceDetail
		public ActionResult Index(long invoiceId)
        {
			var invoice = _invoiceMagager.GetById(invoiceId);
			var details = invoice.GetDetail();
			var viewModels = new List<InvoiceDetailViewModel>();

			AutoMapper.Mapper.Map(details, viewModels);

			ViewBag.InvoiceId = invoiceId;

			return View(viewModels);
        }

        // GET: InvoiceDetail/Details/5
        public ActionResult Details(long id, long invoiceId)
        {
			var invoice = _invoiceMagager.GetById(invoiceId);
			var detail = invoice.GetDetail().First(x => x.Id == id);

			var viewModel = new InvoiceDetailViewModel();

			AutoMapper.Mapper.Map(detail, viewModel);

			return View(viewModel);
        }

        // GET: InvoiceDetail/Create
        public ActionResult Create(long invoiceId)
        {
			ViewBag.InvoiceId = invoiceId;

			return View();
        }

        // POST: InvoiceDetail/Create
        [HttpPost]
        public ActionResult Create(InvoiceDetailViewModel viewModel)
        {
			if (!ModelState.IsValid)
			{
				return View();
			}

			var invoice = _invoiceMagager.GetById(viewModel.InvoiceId);
			ViewBag.InvoiceId = invoice.Id;

			var detail = invoice.InvoiceDetailById(viewModel.Id);

			if (detail != null)
			{
				ModelState.AddModelError(string.Empty, "Invoice Detail already exists");
				
				return View();
			}

			try
            {
				var invoiceDetail = new InvoiceDetail();

				invoiceDetail.Invoice = invoice;
				AutoMapper.Mapper.Map(viewModel, invoiceDetail);
				invoice.AddDetail(invoiceDetail);

				return RedirectToAction("Index", new { invoiceId = invoice.Id});
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceDetail/Edit/5
        public ActionResult Edit(long id, long invoiceId)
        {
			var invoice = _invoiceMagager.GetById(invoiceId);
			var detail = invoice.GetDetail().First(x => x.Id == id);

			var viewModel = new InvoiceDetailViewModel();

			AutoMapper.Mapper.Map(detail, viewModel);

			return View(viewModel);
		}

        // POST: InvoiceDetail/Edit/5
        [HttpPost]
        public ActionResult Edit(InvoiceDetailViewModel viewModel)
        {
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
            {
				var invoice = _invoiceMagager.GetById(viewModel.InvoiceId);
				var detail = invoice.InvoiceDetailById(viewModel.Id);

				AutoMapper.Mapper.Map(viewModel, detail);

				return RedirectToAction("Index", new { invoiceId = invoice.Id });
			}
            catch
            {
                return View();
            }
        }

        // GET: InvoiceDetail/Delete/5
        public ActionResult Delete(long id, long invoiceId)
        {
			var invoice = _invoiceMagager.GetById(invoiceId);
			var detail = invoice.GetDetail().First(x => x.Id == id);

			var viewModel = new InvoiceDetailViewModel();

			AutoMapper.Mapper.Map(detail, viewModel);

			return View(viewModel);
		}

        // POST: InvoiceDetail/Delete/5
        [HttpPost]
        public ActionResult Delete(long id, long invoiceId, FormCollection collection)
        {
            try
            {
				var invoice = _invoiceMagager.GetById(invoiceId);
				var detail = invoice.InvoiceDetailById(id);
				invoice.DeleteDetail(detail);

				return RedirectToAction("Index", new { invoiceId = invoice.Id });
            }
            catch
            {
                return View();
            }
        }
    }
}
