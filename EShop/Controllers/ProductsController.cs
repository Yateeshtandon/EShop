using EShop.Data;
using EShop.Models;
using System.Net;
using System.Web.Mvc;

namespace EShop.Controllers
{
	public class ProductsController : Controller
	{
		private EShopDBEntities db = new EShopDBEntities();

		// GET: Products/Details/5
		public ActionResult Details(int? id, int? customerId)
		{
			if (id == null || customerId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(ProductModel.Map(product, db.Customers.Find(customerId)));
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
