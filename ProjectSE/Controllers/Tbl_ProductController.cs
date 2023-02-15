using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectSE.Models;

namespace ProjectSE.Controllers
{
    public class Tbl_ProductController : Controller
    {
        private eStoreMainEntities db = new eStoreMainEntities();

        // GET: Tbl_Product
        public ActionResult Index()
        {
            var tbl_Product = db.Tbl_Product.Include(t => t.Tbl_Category);
            return View(tbl_Product.ToList());
        }

        public ActionResult Product_list()
        {
            var tbl_Product = db.Tbl_Product.Include(t => t.Tbl_Category);
            return View(tbl_Product.ToList());
        }

        // GET: Tbl_Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product tbl_Product = db.Tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }

        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product tbl_Product = db.Tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }

        // GET: Tbl_Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Tbl_Category, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Tbl_Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,ProductDesc,CategoryId,CreatedDate,ModifiedDate,ProductImage,ImageFile,IsFeatured,Quantity,Price")] Tbl_Product tbl_Product)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(tbl_Product.ImageFile.FileName);
                string extension = Path.GetExtension(tbl_Product.ImageFile.FileName);
                HttpPostedFileBase postedFile = tbl_Product.ImageFile;
                int Length = postedFile.ContentLength;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if (Length <= 1000000)
                    {
                        fileName = fileName + extension;
                        tbl_Product.ProductImage = "~/images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                        tbl_Product.ImageFile.SaveAs(fileName);
                        db.Tbl_Product.Add(tbl_Product);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            ViewBag.Message = "<script>alert('Record Inserted !!')</script>";
                            ModelState.Clear();
                        }
                        else
                        {
                            ViewBag.Message = "<script>alert('Record Not Inserted !!')</script>";
                        }
                    }
                    else
                    {
                        ViewBag.SizeMessage = "<script>alert('Size should be of 1  MB')</script>";
                    }
                }
                else
                {
                    ViewBag.ExtensionMessage = "<script>alert('Image Not Supported !!')</script>";
                }
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Tbl_Category, "CategoryId", "CategoryName", tbl_Product.CategoryId);
            return View(tbl_Product);
        }

        // GET: Tbl_Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product tbl_Product = db.Tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Tbl_Category, "CategoryId", "CategoryName", tbl_Product.CategoryId);
            return View(tbl_Product);
        }

        // POST: Tbl_Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,ProductDesc,CategoryId,CreatedDate,ModifiedDate,ProductImage,IsFeatured,Quantity,Price")] Tbl_Product tbl_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Tbl_Category, "CategoryId", "CategoryName", tbl_Product.CategoryId);
            return View(tbl_Product);
        }

        // GET: Tbl_Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product tbl_Product = db.Tbl_Product.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }

        // POST: Tbl_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Product tbl_Product = db.Tbl_Product.Find(id);
            db.Tbl_Product.Remove(tbl_Product);
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
