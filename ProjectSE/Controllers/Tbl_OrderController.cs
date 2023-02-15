using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectSE.Models;

namespace ProjectSE.Controllers
{
    public class Tbl_OrderController : Controller
    {
        private eStoreMainEntities db = new eStoreMainEntities();

        // GET: Tbl_Order
        public ActionResult Index()
        {
            var tbl_Order = db.Tbl_Order.Include(t => t.Tbl_Product).Include(t => t.Tbl_ShippingDetail);
            return View(tbl_Order.ToList());
        }
        public ActionResult CustomerView(int id)
        {
            TempData["orPval"] = id;
            var tbl_Order = db.Tbl_Order.Include(t => t.Tbl_Product).Include(t => t.Tbl_ShippingDetail);
            return View(tbl_Order.ToList());
        }

        // GET: Tbl_Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Order tbl_Order = db.Tbl_Order.Find(id);
            if (tbl_Order == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Order);
        }

        public ActionResult DirectEnter()
        {
            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductId");
            ViewBag.ShippingID = new SelectList(db.Tbl_ShippingDetail, "ShippingDetailId", "ShippingDetailId");
            return View();
        }

        [HttpPost]
        public ActionResult DirectEnter( [Bind(Include = "OrderId,ShippingID,ProductId")] Tbl_Order tbl_Order)
        {
            List<int> val = (List<int>)Session["crtids"];
            int id = Convert.ToInt32(Session["shid"]);
            while(val.Count != 0)
            {
                tbl_Order.ProductId = val[0];
                tbl_Order.ShippingID = id;

                if (ModelState.IsValid)
                {
                    db.Tbl_Order.Add(tbl_Order);
                    db.SaveChanges();
                    val.RemoveAt(0);
                }
            }
                

            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductId", tbl_Order.ProductId);
            ViewBag.ShippingID = new SelectList(db.Tbl_ShippingDetail, "ShippingDetailId", "ShippingDetailId", tbl_Order.ShippingID);
            return View(tbl_Order);
        }

        // GET: Tbl_Order/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductId");
            ViewBag.ShippingID = new SelectList(db.Tbl_ShippingDetail, "ShippingDetailId", "ShippingDetailId");
            return View();
        }

        // POST: Tbl_Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,ShippingID,ProductId")] Tbl_Order tbl_Order)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Order.Add(tbl_Order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductId", tbl_Order.ProductId);
            ViewBag.ShippingID = new SelectList(db.Tbl_ShippingDetail, "ShippingDetailId", "ShippingDetailId", tbl_Order.ShippingID);
            return View(tbl_Order);
        }

        // GET: Tbl_Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Order tbl_Order = db.Tbl_Order.Find(id);
            if (tbl_Order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductName", tbl_Order.ProductId);
            ViewBag.ShippingID = new SelectList(db.Tbl_ShippingDetail, "ShippingDetailId", "Address", tbl_Order.ShippingID);
            return View(tbl_Order);
        }

        // POST: Tbl_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,ShippingID,ProductId")] Tbl_Order tbl_Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductName", tbl_Order.ProductId);
            ViewBag.ShippingID = new SelectList(db.Tbl_ShippingDetail, "ShippingDetailId", "Address", tbl_Order.ShippingID);
            return View(tbl_Order);
        }

        // GET: Tbl_Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Order tbl_Order = db.Tbl_Order.Find(id);
            if (tbl_Order == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Order);
        }

        // POST: Tbl_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Order tbl_Order = db.Tbl_Order.Find(id);
            db.Tbl_Order.Remove(tbl_Order);
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
