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
    public class Tbl_MemberController : Controller
    {
        private eStoreMainEntities db = new eStoreMainEntities();

        // GET: Tbl_Member
        public ActionResult Index()
        {
            return View(db.Tbl_Member.ToList());
        }

        public ActionResult memberLogin(Tbl_Member lg)
        {
            if (ModelState.IsValid == true)
            {
                var credential = db.Tbl_Member.Where(model => model.EmailId ==
               lg.EmailId && model.Password == lg.Password).FirstOrDefault();
                if (credential == null)
                {
                    ViewBag.Errormsg = "";
                    return View();
                }
                else
                { 
                    Session["UserName"] = lg.EmailId;
                    Session["Member"] = lg.FirstName;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        // GET: Tbl_Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Member tbl_Member = db.Tbl_Member.Find(id);
            if (tbl_Member == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Member);
        }

        // GET: Tbl_Member/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tbl_Member/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberId,FirstName,LastName,EmailId,Password,CreatedOn,ModifiedOn")] Tbl_Member tbl_Member)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Member.Add(tbl_Member);
                db.SaveChanges();
                return RedirectToAction("SelectLog", "Home");
            }

            return View(tbl_Member);
        }

        // GET: Tbl_Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Member tbl_Member = db.Tbl_Member.Find(id);
            if (tbl_Member == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Member);
        }

        // POST: Tbl_Member/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,FirstName,LastName,EmailId,Password,CreatedOn,ModifiedOn")] Tbl_Member tbl_Member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Member);
        }

        // GET: Tbl_Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Member tbl_Member = db.Tbl_Member.Find(id);
            if (tbl_Member == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Member);
        }

        // POST: Tbl_Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Member tbl_Member = db.Tbl_Member.Find(id);
            db.Tbl_Member.Remove(tbl_Member);
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
