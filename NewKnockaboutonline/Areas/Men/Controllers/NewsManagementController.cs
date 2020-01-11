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
    public class NewsManagementController : Controller
    {
        private KnockaboutonlineEntities db = new KnockaboutonlineEntities();

        // GET: Common/NewsManagement
        public ActionResult Index()
        {
            var nEWS = db.NEWS.Include(n => n.USER);
            return View(nEWS.ToList());
        }

        // GET: Common/NewsManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NEWS nEWS = db.NEWS.Find(id);
            if (nEWS == null)
            {
                return HttpNotFound();
            }
            return View(nEWS);
        }

        // GET: Common/NewsManagement/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.USER, "UserId", "Username");
            return View();
        }

        // POST: Common/NewsManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,UserId,Title,ShortDescription,Image,Content,CreatedDate,Status")] NEWS nEWS)
        {
            if (ModelState.IsValid)
            {
                db.NEWS.Add(nEWS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.USER, "UserId", "Username", nEWS.UserId);
            return View(nEWS);
        }

        // GET: Common/NewsManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NEWS nEWS = db.NEWS.Find(id);
            if (nEWS == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.USER, "UserId", "Username", nEWS.UserId);
            return View(nEWS);
        }

        // POST: Common/NewsManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,UserId,Title,ShortDescription,Image,Content,CreatedDate,Status")] NEWS nEWS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nEWS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.USER, "UserId", "Username", nEWS.UserId);
            return View(nEWS);
        }

        // GET: Common/NewsManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NEWS nEWS = db.NEWS.Find(id);
            if (nEWS == null)
            {
                return HttpNotFound();
            }
            return View(nEWS);
        }

        // POST: Common/NewsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NEWS nEWS = db.NEWS.Find(id);
            db.NEWS.Remove(nEWS);
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