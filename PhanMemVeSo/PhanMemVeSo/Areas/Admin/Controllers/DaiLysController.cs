using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EFModels;

namespace PhanMemVeSo.Areas.Admin.Controllers
{
    public class DaiLysController : Controller
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();

        // GET: Admin/DaiLys
        public ActionResult Index()
        {
            return View(db.DaiLies.ToList());
        }

        // GET: Admin/DaiLys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daiLy = db.DaiLies.Find(id);
            if (daiLy == null)
            {
                return HttpNotFound();
            }
            return View(daiLy);
        }

        // GET: Admin/DaiLys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DaiLys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DaiLyId,TenDaiLy")] DaiLy daiLy)
        {
            if (ModelState.IsValid)
            {
                db.DaiLies.Add(daiLy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(daiLy);
        }

        // GET: Admin/DaiLys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daiLy = db.DaiLies.Find(id);
            if (daiLy == null)
            {
                return HttpNotFound();
            }
            return View(daiLy);
        }

        // POST: Admin/DaiLys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DaiLyId,TenDaiLy")] DaiLy daiLy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daiLy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(daiLy);
        }

        // GET: Admin/DaiLys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daiLy = db.DaiLies.Find(id);
            if (daiLy == null)
            {
                return HttpNotFound();
            }
            return View(daiLy);
        }

        // POST: Admin/DaiLys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DaiLy daiLy = db.DaiLies.Find(id);
            db.DaiLies.Remove(daiLy);
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
