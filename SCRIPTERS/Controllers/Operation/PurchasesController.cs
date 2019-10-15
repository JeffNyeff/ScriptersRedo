using System.Collections.Generic;
using System.Web.Mvc;
using Rotativa;
using SCRIPTERS.BLL;
using SCRIPTERS.BLL.Operation;
using SCRIPTERS.Core.Models;
using SCRIPTERS.Core.Models.Operation;

namespace SCRIPTERS.Controllers.Operation
{
    //[Authorize]
    public class PurchasesController : Controller
    {

        PurchaseBll purchaseBll = new PurchaseBll();
        BookBll itemBll = new BookBll();
        bool status = false;
        int id;
        // GET: Purchases
        public ActionResult List()
        {
            List<Purchase> Purchases = purchaseBll.List();
            return View(Purchases);
        }
        //GET: Details/5
        public ActionResult Details(int id)
        {
            Purchase purchase = purchaseBll.Details(id);
            return View(purchase);
        }
        public ActionResult DetailsPdf(int id)
        {
            Purchase purchase = purchaseBll.Details(id);
            return View(purchase);
        }

        //GET: Create

        public ActionResult Create()
        {
            ViewBag.OutletId = purchaseBll.GetOutlet();
            ViewBag.EmployeeId = purchaseBll.GetEmployee(); //TO DO: change to get the currently logged in Employee
            ViewBag.ItemId = purchaseBll.GetItem();
            ViewBag.Supplier = purchaseBll.Supplier();
            ViewBag.PurchaseCode = purchaseBll.GenerateAutoCode();

            return View();
        }

        // POST: Create
        [HttpPost]
        public ActionResult Create(Purchase purchase)
        {
            purchase.IsDeleted = false;
            if (ModelState.IsValid && purchase.PurchaseDetail != null && purchase.PurchaseDetail.Count > 0)
            {
                id = purchaseBll.Create(purchase);
                if (id != null)
                {
                    System.Diagnostics.Debug.WriteLine(purchase.PurchaseDetail);
                    return RedirectToAction("Details", "Purchases", new { id = id });
                }
                else
                {
                    ViewBag.Message = "Purchase added failed";
                }
            }
            ViewBag.ItemId = purchaseBll.GetItem();
            ViewBag.OutletId = purchaseBll.GetOutlet();
            ViewBag.EmployeeId = purchaseBll.GetEmployee();
            ViewBag.Supplier = purchaseBll.Supplier();
            return View(purchase);
        }

        public ActionResult ExportPdf(int id)
        {
            return new ActionAsPdf("DetailsPdf", new { id = id });
        }

        public JsonResult GetItemSalesPrice(int id)
        {
            Book item = itemBll.GetById(id);
            var itemPrice = item.SalePrice;
            return Json(itemPrice);
        }
        // GET: Items/Delete/5

        public JsonResult Delete(int id)
        {
            status = purchaseBll.Delete(id);
            if (status == true)
            {
                return Json(1);
            }
            return Json(0);
        }
    }
}