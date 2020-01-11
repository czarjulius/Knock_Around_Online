using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewKnockaboutonline.Models;
using PagedList;

namespace NewKnockaboutonline.Areas.Men.Controllers
{
    public class ProductController : Controller
    {
        private KnockaboutonlineEntities db;
        public ProductController()
        {
            db = new KnockaboutonlineEntities();
        }



        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult _ProductList(int? page, int? category)
        {
            var pageNumber = page ?? 1;
            var pageSize = 8;
            if (category != null)
            {
                ViewBag.category = category;
                var productList = db.PRODUCT
                    .OrderBy(x => x.ProductId)
                    .Where(x => x.CategoryId == category)
                    .ToPagedList(pageNumber, pageSize);
                return PartialView(productList);
            }
            else
            {
                var productList = db.PRODUCT.OrderBy(x => x.ProductId).ToPagedList(pageNumber, pageSize);
                return PartialView(productList);
            }

        }

        public PartialViewResult _SlideProduct()
        {
           
                var productList = db.PRODUCT.OrderBy(x => x.ProductId).ToList();
                return PartialView(productList);
            

        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT product = db.PRODUCT.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        //This are all the forign keys to drop as menu
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.CATEGORY, "CategoryId", "Name");
            //List<CATEGORY> categories = db.CATEGORY.ToList();
            //ViewBag.category = categories;
            ViewBag.Size = new SelectList(db.SIZE, "SizeId", "Name");
            ViewBag.Storage = new SelectList(db.STORAGE, "StorageId", "Storage1");
            ViewBag.Color = new SelectList(db.COLOR, "ColorId", "Color1");

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PRODUCT model)
        {
            if (ModelState.IsValid)
            {
                string fileName1 = Path.GetFileNameWithoutExtension(model.ImageBgFile.FileName);
                string fileName2 = Path.GetFileNameWithoutExtension(model.ImageSm1File.FileName);
                string fileName3 = Path.GetFileNameWithoutExtension(model.ImageSm2File.FileName);
                string fileName4 = Path.GetFileNameWithoutExtension(model.ImageSm3File.FileName);
                string fileName5 = Path.GetFileNameWithoutExtension(model.ImageSm4File.FileName);

                string extension1 = Path.GetExtension(model.ImageBgFile.FileName);
                string extension2 = Path.GetExtension(model.ImageSm1File.FileName);
                string extension3 = Path.GetExtension(model.ImageSm2File.FileName);
                string extension4 = Path.GetExtension(model.ImageSm3File.FileName);
                string extension5 = Path.GetExtension(model.ImageSm4File.FileName);

                fileName1 = fileName1 + DateTime.Now.ToString("ÿymmssff") + extension1;
                fileName2 = fileName2 + DateTime.Now.ToString("ÿymmssff") + extension2;
                fileName3 = fileName3 + DateTime.Now.ToString("ÿymmssff") + extension3;
                fileName4 = fileName4 + DateTime.Now.ToString("ÿymmssff") + extension4;
                fileName5 = fileName5 + DateTime.Now.ToString("ÿymmssff") + extension5;

                model.ImageBg = fileName1;
                model.ImageSm1 = fileName2;
                model.ImageSm2 = fileName3;
                model.ImageSm3 = fileName4;
                model.ImageSm4 = fileName5;

                model.ImageBgFile.SaveAs(Path.Combine(Server.MapPath("~/Media/Products"), fileName1));
                model.ImageSm1File.SaveAs(Path.Combine(Server.MapPath("~/Media/Products"), fileName2));
                model.ImageSm2File.SaveAs(Path.Combine(Server.MapPath("~/Media/Products"), fileName3));
                model.ImageSm3File.SaveAs(Path.Combine(Server.MapPath("~/Media/Products"), fileName4));
                model.ImageSm4File.SaveAs(Path.Combine(Server.MapPath("~/Media/Products"), fileName5));


                PRODUCT item = new PRODUCT();

                item.ProductName = model.ProductName;

                item.ImageBg = fileName1;
                item.ImageSm1 = fileName2;
                item.ImageSm2 = fileName3;
                item.ImageSm3 = fileName4;
                item.ImageSm4 = fileName5;

                item.Price = model.Price;
                item.CategoryId = model.CategoryId;
                item.ColorId = model.ColorId;
                item.StorageId = model.StorageId;
                item.SellStartDate = DateTime.Now;
                item.SellEndDate = DateTime.Now;
                item.IsNew = model.IsNew;
                item.SizeId = model.SizeId;

                db.PRODUCT.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View();

        }
    }
}