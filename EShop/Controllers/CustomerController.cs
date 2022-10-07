using EShop.Data;
using EShop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EShop.Controllers
{
	public class CustomerController : Controller
	{
		private readonly EShopDBEntities db = new EShopDBEntities();

		// GET: Customer
		public ActionResult Index()
		{
			var customers = new List<CustomerModel>();

			foreach (var c in db.Customers)
			{
				customers.Add(CustomerModel.Map(c));
			}

			return View(customers);
		}

		// GET: Customer/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Customer customer = db.Customers.Find(id);
			if (customer == null)
			{
				return HttpNotFound();
			}

			return View(CustomerModel.Map(customer));
		}

		// GET: Customer/Create
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CustomerModel model)
		{
			if (ModelState.IsValid)
			{
				Customer c = CustomerModel.Map(model);
				db.Customers.Add(c);

				var result = db.SaveChanges();

				foreach (var p in db.Products)
				{
					c.ProductCustomers.Add(new ProductCustomer()
					{
						CustomerId = c.CustomerId,
						ProductId = p.ProductId
					});
				}

				result = db.SaveChanges();

				if (result > 0)
				{
					TempData["InsertMessage"] = "<script>alert('Customer Added Sucessfully....')</script>";
					ModelState.Clear();
					return RedirectToAction("Index", "Customer");
				}
				else
				{
					TempData["InsertMessage"] = "<script>alert('Error In Inserting Record....')</script>";
				}

				return RedirectToAction("Index");
			}

			return View();
		}

		// GET: Customer/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Customer customer = db.Customers.Find(id);
			if (customer == null)
			{
				return HttpNotFound();
			}
			return View(CustomerModel.Map(customer));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(CustomerModel model)
		{
			if (ModelState.IsValid)
			{
				Customer c = CustomerModel.Map(model);

				db.Entry(c).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}

		// GET: Customer/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Customer customer = db.Customers.Find(id);
			if (customer == null)
			{
				return HttpNotFound();
			}
			return View(CustomerModel.Map(customer));
		}

		// POST: Customer/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Customer customer = db.Customers.Find(id);

			foreach (var cp in customer.ProductCustomers.ToList())
			{
				db.ProductCustomers.Remove(cp);
			}

			db.Customers.Remove(customer);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
