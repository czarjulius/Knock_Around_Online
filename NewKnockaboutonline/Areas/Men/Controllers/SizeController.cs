using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewKnockaboutonline.Models;

namespace NewKnockaboutonline.Areas.Men.Controllers
{
    public class SizeController : Controller
    {
        private KnockaboutonlineEntities db = new KnockaboutonlineEntities();

        // GET: Common/Size
        public ActionResult Index()
        {
            return View(db.SIZE.ToList());
        }

        // GET: Common/Size/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SIZE sIZE = db.SIZE.Find(id);
            if (sIZE == null)
            {
                return HttpNotFound();
            }
            return View(sIZE);
        }

        // GET: Common/Size/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Common/Size/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SizeId,Name")] SIZE sIZE)
        {
            if (ModelState.IsValid)
            {
                db.SIZE.Add(sIZE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sIZE);
        }

        // GET: Common/Size/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SIZE sIZE = db.SIZE.Find(id);
            if (sIZE == null)
            {
                return HttpNotFound();
            }
            return View(sIZE);
        }

        // POST: Common/Size/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SizeId,Name")] SIZE sIZE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sIZE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sIZE);
        }

        // GET: Common/Size/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SIZE sIZE = db.SIZE.Find(id);
            if (sIZE == null)
            {
                return HttpNotFound();
            }
            return View(sIZE);
        }

        // POST: Common/Size/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SIZE sIZE = db.SIZE.Find(id);
            db.SIZE.Remove(sIZE);
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