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
    public class ColorController : Controller
    {
        private KnockaboutonlineEntities db = new KnockaboutonlineEntities();

        // GET: Common/Color
        public ActionResult Index()
        {
            return View(db.COLOR.ToList());
        }

        // GET: Common/Color/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLOR cOLOR = db.COLOR.Find(id);
            if (cOLOR == null)
            {
                return HttpNotFound();
            }
            return View(cOLOR);
        }

        // GET: Common/Color/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Common/Color/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColorId,Color1")] COLOR cOLOR)
        {
            if (ModelState.IsValid)
            {
                db.COLOR.Add(cOLOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOLOR);
        }

        // GET: Common/Color/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLOR cOLOR = db.COLOR.Find(id);
            if (cOLOR == null)
            {
                return HttpNotFound();
            }
            return View(cOLOR);
        }

        // POST: Common/Color/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColorId,Color1")] COLOR cOLOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOLOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOLOR);
        }

        // GET: Common/Color/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLOR cOLOR = db.COLOR.Find(id);
            if (cOLOR == null)
            {
                return HttpNotFound();
            }
            return View(cOLOR);
        }

        // POST: Common/Color/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COLOR cOLOR = db.COLOR.Find(id);
            db.COLOR.Remove(cOLOR);
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