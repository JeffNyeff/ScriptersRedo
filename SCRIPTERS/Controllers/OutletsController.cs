﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCRIPTERS.BLL;

using SCRIPTERS.Core.Models;
using SCRIPTERS.Models;

namespace SCRIPTERS.Controllers
{
    [Authorize(Roles = "Manager")]
    public class OutletsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Common common = new Common();
        OutletBll _outletBll = new OutletBll();
        bool status = false;

        // GET: Outlets

        #region AutogeneratedCode
        /*
         *    public ActionResult Index()
        {
            return View(db.Outlets.ToList());
        }

        // GET: Outlets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            return View(outlet);
        }

        // GET: Outlets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Outlets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,ContactNo,Address")] Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                db.Outlets.Add(outlet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(outlet);
        }

        // GET: Outlets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            return View(outlet);
        }

        // POST: Outlets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,ContactNo,Address")] Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outlet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(outlet);
        }

        // GET: Outlets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            return View(outlet);
        }

        // POST: Outlets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Outlet outlet = db.Outlets.Find(id);
            db.Outlets.Remove(outlet);
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
         */

        #endregion

        // GET: Items
        public ActionResult List()
        {
            List<Outlet> Outlets = _outletBll.List();
            return View(Outlets);
        }

        // GET: Items/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.autoCode = _outletBll.GenerateAutoCode();
           // ViewBag.OrganizationId = outletBll.GetOrganization();
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                status = _outletBll.Create(outlet);
                if (status == true)
                {
                    return RedirectToAction("List", "Outlets");
                }
                else
                {
                    ViewBag.Message = "Expense Catagory added failed";
                }
            }
            //ViewBag.OrganizationId = outletBll.GetOrganization();
            return View(outlet);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }
            Outlet outlet = _outletBll.GetById(id);
            if (outlet == null)
            {
                return RedirectToAction("Error", "Home");
            }
            //    ViewBag.OrganizationId = outletBll.GetOrganization();
            return View(outlet);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                status = _outletBll.Edit(outlet);
                if (status == true)
                {
                    return RedirectToAction("List", "Outlets");
                }
                else
                {
                    ViewBag.Message = "Item is not updated succesfully";
                }
            }

            //ViewBag.OrganizationId = outletBll.GetOrganization();
            return View(outlet);

        }
        // GET: Items/Delete/5
        public JsonResult Delete(int id)
        {
            status = _outletBll.Delete(id);
            if (status == true)
            {
                return Json(1);
            }
            return Json(0);
        }

    }
}
