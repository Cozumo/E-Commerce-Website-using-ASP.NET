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
    public class Tbl_AdminController : Controller
    {
        private eStoreMainEntities db = new eStoreMainEntities();

        // GET: Tbl_Admin
        public ActionResult Index()
        {
            return View(db.Tbl_Admin.ToList());
        }

        public ActionResult adminLogin(Tbl_Admin lg)
        {
            if (ModelState.IsValid == true)
            {
                var credential = db.Tbl_Admin.Where(model => model.Adminid ==
               lg.Adminid && model.pass == lg.pass).FirstOrDefault();
                if (credential == null)
                {
                    ViewBag.Errormsg = "";
                    return View();
                }
                else
                {
                    return RedirectToAction("Dashboard", "Home");
                }
            }
            return View();
        }

        // GET: Tbl_Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Admin tbl_Admin = db.Tbl_Admin.Find(id);
            if (tbl_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Admin);
        }

        // GET: Tbl_Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tbl_Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Adminid,pass")] Tbl_Admin tbl_Admin)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Admin.Add(tbl_Admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Admin);
        }

        // GET: Tbl_Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Admin tbl_Admin = db.Tbl_Admin.Find(id);
            if (tbl_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Admin);
        }

        // POST: Tbl_Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Adminid,pass")] Tbl_Admin tbl_Admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Admin);
        }

        // GET: Tbl_Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Admin tbl_Admin = db.Tbl_Admin.Find(id);
            if (tbl_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Admin);
        }

        // POST: Tbl_Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Admin tbl_Admin = db.Tbl_Admin.Find(id);
            db.Tbl_Admin.Remove(tbl_Admin);
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
