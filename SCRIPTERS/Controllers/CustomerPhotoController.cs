﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCRIPTERS.Controllers
{
    public class CustomerPhotoController : Controller
    {
        // GET: CustomerPhoto
        [HttpGet]
        public ActionResult Index()
        {
            Session["val"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Imagename)
        {
            string sss = Session["val"].ToString();

            ViewBag.pic = "https://localhost:44321/CustomerImages/" + Session["val"].ToString();

            return View();
        }

        [HttpGet]
        public ActionResult Changephoto()
        {
            if (Convert.ToString(Session["val"]) != string.Empty)
            {
                ViewBag.pic = "https://localhost:44321/CustomerImages/" + Session["val"].ToString();
            }
            else
            {
                ViewBag.pic = "../../CustomerImages/person.jpg";
            }
            return View();
        }


        public JsonResult Rebind()
        {
            string path = "https://localhost:44321/CustomerImages/" + Session["val"].ToString();

            return Json(path, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Capture()
        {
            var stream = Request.InputStream;
            string dump;

            using (var reader = new StreamReader(stream))
            {
                dump = reader.ReadToEnd();

                DateTime nm = DateTime.Now;

                string date = nm.ToString("yyyy-mm-dd-MMss");

                var path = Server.MapPath("~/CustomerImages/" + date + "Customer.jpg");

                System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));

                ViewData["path"] = date + "Customer.jpg";

                Session["val"] = date + "Customer.jpg";
            }

            return View("Index");
        }

        private byte[] String_To_Bytes2(string strInput)
        {
            int numBytes = (strInput.Length) / 2;

            byte[] bytes = new byte[numBytes];

            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }

            return bytes;
        }
    }
}