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
    public class Tbl_ShippingDetailController : Controller
    {
        private eStoreMainEntities db = new eStoreMainEntities();

        // GET: Tbl_ShippingDetail
        public ActionResult Index()
        {
            var tbl_ShippingDetail = db.Tbl_ShippingDetail.Include(t => t.Tbl_Member).Include(t => t.Tbl_Order);
            return View(tbl_ShippingDetail.ToList());
        }

        public ActionResult CustomerView()
        {
            var tbl_ShippingDetail = db.Tbl_ShippingDetail.Include(t => t.Tbl_Member).Include(t => t.Tbl_Order);
            return View(tbl_ShippingDetail.ToList());
        }

        public ActionResult Checkout()
        {
            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName");
            ViewBag.OrderId = new SelectList(db.Tbl_Order, "OrderId", "OrderId");
            return View();
        }
        [HttpPost]
        public ActionResult Checkout(string em, [Bind(Include = "ShippingDetailId,Address,City,State,Country,ZipCode,OrderId,PaymentType,ExpiryDate,Total")] Tbl_ShippingDetail tbl_ShippingDetail)
        {
            var credential = db.Tbl_Member.Where(model => model.EmailId == em).FirstOrDefault();
            tbl_ShippingDetail.MemberId = credential.MemberId;
            tbl_ShippingDetail.Total = Convert.ToDecimal(Session["Total"]);
            if (ModelState.IsValid)
            {
                db.Tbl_ShippingDetail.Add(tbl_ShippingDetail);
                db.SaveChanges();
                Session["shid"] = tbl_ShippingDetail.ShippingDetailId;
                return RedirectToAction("DirectEnter", "Tbl_Order");
            }

            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName", tbl_ShippingDetail.MemberId);
            return View(tbl_ShippingDetail);
        }

        // GET: Tbl_ShippingDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_ShippingDetail tbl_ShippingDetail = db.Tbl_ShippingDetail.Find(id);
            if (tbl_ShippingDetail == null)
            {
                return HttpNotFound();
            }
            return View(tbl_ShippingDetail);
        }

        // GET: Tbl_ShippingDetail/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName");
            ViewBag.OrderId = new SelectList(db.Tbl_Order, "OrderId", "OrderId");
            return View();
        }

        // POST: Tbl_ShippingDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShippingDetailId,MemberId,Adress,City,State,Country,ZipCode,OrderId,PaymentType")] Tbl_ShippingDetail tbl_ShippingDetail)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_ShippingDetail.Add(tbl_ShippingDetail);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName", tbl_ShippingDetail.MemberId);
            return View(tbl_ShippingDetail);
        }

        // GET: Tbl_ShippingDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_ShippingDetail tbl_ShippingDetail = db.Tbl_ShippingDetail.Find(id);
            if (tbl_ShippingDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName", tbl_ShippingDetail.MemberId);
            return View(tbl_ShippingDetail);
        }

        // POST: Tbl_ShippingDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShippingDetailId,MemberId,Adress,City,State,Country,ZipCode,OrderId,AmountPaid,PaymentType")] Tbl_ShippingDetail tbl_ShippingDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_ShippingDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Tbl_Member, "MemberId", "FirstName", tbl_ShippingDetail.MemberId);
            return View(tbl_ShippingDetail);
        }

        // GET: Tbl_ShippingDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_ShippingDetail tbl_ShippingDetail = db.Tbl_ShippingDetail.Find(id);
            if (tbl_ShippingDetail == null)
            {
                return HttpNotFound();
            }
            return View(tbl_ShippingDetail);
        }

        // POST: Tbl_ShippingDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_ShippingDetail tbl_ShippingDetail = db.Tbl_ShippingDetail.Find(id);
            db.Tbl_ShippingDetail.Remove(tbl_ShippingDetail);
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
