using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewKnockaboutonline.Models;
using PagedList;

namespace NewKnockaboutonline.Areas.Men.Controllers
{
    public class OrderController : Controller
    {
        KnockaboutonlineEntities db = new KnockaboutonlineEntities();

        // GET: Common/Order
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;
            var orderList = db.ORDER.OrderByDescending(x => x.OrderId).ToPagedList(pageNumber, pageSize);
            return View(orderList);
        }
        // GET: Common/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.ORDER.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Common/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Common/Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Common/Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Common/Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Common/Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Common/Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}