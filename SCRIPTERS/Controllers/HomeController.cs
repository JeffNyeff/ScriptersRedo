using SCRIPTERS.Core.Models;
using SCRIPTERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace SCRIPTERS.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
                 
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult GetData()
        {
            int book = context.Inventories.Where(x => x.ItemCategoryId == 6).Count();
            int stationery = context.Inventories.Where(x => x.ItemCategoryId == 4).Count();
            int accessories = context.Inventories.Where(x => x.ItemCategoryId == 3).Count();
            //int tap_to_pay = context.Inventory_Type.Where(x => x.InventoryType_Name == "Tap-to-pay").Count();
            Ratio obj = new Ratio();
            obj.Book = book;
            obj.Stationery = stationery;
            obj.Accessories = accessories;
            //obj.Tap_to_pay = tap_to_pay;



            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public Ratio PiechartValues()
        {

            int book = context.Inventories.Where(x => x.ItemCategoryId == 8).Count();
            int stationery = context.Inventories.Where(x => x.ItemCategoryId == 4).Count();
            int accessories = context.Inventories.Where(x => x.ItemCategoryId == 3).Count();
            //int tap_to_pay = context.Inventory_Type.Where(x => x.InventoryType_Name == "Tap-to-pay").Count();
            Ratio obj = new Ratio();
            obj.Book = book;
            obj.Stationery = stationery;
            obj.Accessories = accessories;

            return obj;
        }
        public class Ratio
        {
            public int Book { get; set; }
            public int Stationery { get; set; }
            public int Accessories { get; set; }
            //public int Tap_to_pay { get; set; }
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                if (e.EventId > 0)
                {
                    //Update the event
                    var v = dc.Events.Where(a => a.EventId == e.EventId).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }

                dc.SaveChanges();
                status = true;
                

            }
            return new JsonResult { Data = new { status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var v = dc.Events.Where(a => a.EventId == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status } };
        }
    }
}