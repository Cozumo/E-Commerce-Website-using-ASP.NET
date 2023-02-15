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
    public class Tbl_CartController : Controller
    {
        private eStoreMainEntities db = new eStoreMainEntities();

        // GET: Tbl_Cart
        public ActionResult Index()
        {
            var tbl_Cart = db.Tbl_Cart.Include(t => t.Tbl_Member).Include(t => t.Tbl_Product).Include(t => t.Tbl_Product1);
            return View(tbl_Cart.ToList());
        }

        public ActionResult Cart_List()
        {
            if(Convert.ToString(Session["UserName"]) != "")
            {
                var tbl_Cart = db.Tbl_Cart.Include(t => t.Tbl_Member).Include(t => t.Tbl_Product).Include(t => t.Tbl_Product1);
                return View(tbl_Cart.ToList());
            }
            else
            {
                return RedirectToAction("SelectLog", "Home");
            }
        }
        public ActionResult CustomerCreate()
        {
            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName");
            ViewBag.Price = new SelectList(db.Tbl_Product, "ProductId", "ProductName");
            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductName");
            return View();
        }

        [HttpPost]
        public ActionResult CustomerCreate(int id, string em, [Bind(Include = "CartId,ProductId,MemberId,Quantity,Price")] Tbl_Cart tbl_Cart)
        {
            var credential = db.Tbl_Member.Where(model => model.EmailId == em).FirstOrDefault();
            tbl_Cart.ProductId = id;
            tbl_Cart.MemberId = credential.MemberId;
            if (tbl_Cart.Quantity == 0 || tbl_Cart.Quantity == null)
            {
                tbl_Cart.Quantity = 1;
            }
            if (ModelState.IsValid)
            {
                db.Tbl_Cart.Add(tbl_Cart);
                db.SaveChanges();
                return RedirectToAction("Cart_List", "Tbl_Cart");
            }

            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName");
            ViewBag.Price = new SelectList(db.Tbl_Product, "ProductId", "ProductName");
            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductName");
            return View();
        }

        // GET: Tbl_Cart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Cart tbl_Cart = db.Tbl_Cart.Find(id);
            if (tbl_Cart == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Cart);
        }

        // GET: Tbl_Cart/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName");
            ViewBag.Price = new SelectList(db.Tbl_Product, "ProductId", "ProductName");
            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductName");
            return View();
        }

        // POST: Tbl_Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CartId,ProductId,MemberId,CartStatusId,Price")] Tbl_Cart tbl_Cart)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Cart.Add(tbl_Cart);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName", tbl_Cart.MemberId);
            ViewBag.Price = new SelectList(db.Tbl_Product, "ProductId", "ProductName", tbl_Cart.Price);
            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductName", tbl_Cart.ProductId);
            return View(tbl_Cart);
        }

        // GET: Tbl_Cart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Cart tbl_Cart = db.Tbl_Cart.Find(id);
            if (tbl_Cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName", tbl_Cart.MemberId);
            ViewBag.Price = new SelectList(db.Tbl_Product, "ProductId", "ProductName", tbl_Cart.Price);
            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductName", tbl_Cart.ProductId);
            return View(tbl_Cart);
        }

        // POST: Tbl_Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CartId,ProductId,MemberId,CartStatusId,Price")] Tbl_Cart tbl_Cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName", tbl_Cart.MemberId);
            ViewBag.Price = new SelectList(db.Tbl_Product, "ProductId", "ProductName", tbl_Cart.Price);
            ViewBag.ProductId = new SelectList(db.Tbl_Product, "ProductId", "ProductName", tbl_Cart.ProductId);
            return View(tbl_Cart);
        }

        // GET: Tbl_Cart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Cart tbl_Cart = db.Tbl_Cart.Find(id);
            if (tbl_Cart == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Cart);
        }

        // POST: Tbl_Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Cart tbl_Cart = db.Tbl_Cart.Find(id);
            db.Tbl_Cart.Remove(tbl_Cart);
            db.SaveChanges();
            return RedirectToAction("Cart_list", "Tbl_Cart");
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
