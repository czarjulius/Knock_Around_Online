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
    public class CategoryController : Controller
    {
        private KnockaboutonlineEntities db;
        public CategoryController()
        {
            db = new KnockaboutonlineEntities();
        }
        public ActionResult Index()
        {
            var categories = db.CATEGORY.OrderBy(c => c.Name).ToList();
            return View(categories);
        }

        public PartialViewResult _CategoryPartial()
        {

            var categoryList = db.CATEGORY.OrderBy(x => x.Name).ToList();
            return PartialView(categoryList);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CATEGORY category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CATEGORY.Add(category);
                    db.SaveChanges();
                    TempData["Msg"] = "Category created Successfully";
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Create Failed" + e;
                return RedirectToAction("Index");

            }
        }

        // GET: Category/Edit/1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY category = db.CATEGORY.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        // POST: Category/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name")] CATEGORY category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Msg"] = "Updated Successfully";
                    return RedirectToAction("Index");
                }
                return View(category);

            }
            catch (Exception e)
            {
                TempData["Msg"] = "Update Failed" + e;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = db.CATEGORY.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY category = db.CATEGORY.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        // POST: Category/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var category = db.CATEGORY.Find(id);
                db.CATEGORY.Remove(category);
                db.SaveChanges();
                TempData["Msg"] = "Deleted Successfully";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Delete Failed" + e;
                return RedirectToAction("Index");
            }

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}