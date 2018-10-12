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
    public class LoaiVeSoesController : Controller
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();

        // GET: Admin/LoaiVeSoes
        public ActionResult Index()
        {
            return View(db.LoaiVeSoes.ToList());
        }

        // GET: Admin/LoaiVeSoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVeSo loaiVeSo = db.LoaiVeSoes.Find(id);
            if (loaiVeSo == null)
            {
                return HttpNotFound();
            }
            return View(loaiVeSo);
        }

        // GET: Admin/LoaiVeSoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiVeSoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoaiVeSoId,TenTinh")] LoaiVeSo loaiVeSo)
        {
            if (ModelState.IsValid)
            {
                db.LoaiVeSoes.Add(loaiVeSo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiVeSo);
        }

        // GET: Admin/LoaiVeSoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVeSo loaiVeSo = db.LoaiVeSoes.Find(id);
            if (loaiVeSo == null)
            {
                return HttpNotFound();
            }
            return View(loaiVeSo);
        }

        // POST: Admin/LoaiVeSoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoaiVeSoId,TenTinh")] LoaiVeSo loaiVeSo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiVeSo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiVeSo);
        }

        // GET: Admin/LoaiVeSoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVeSo loaiVeSo = db.LoaiVeSoes.Find(id);
            if (loaiVeSo == null)
            {
                return HttpNotFound();
            }
            return View(loaiVeSo);
        }

        // POST: Admin/LoaiVeSoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiVeSo loaiVeSo = db.LoaiVeSoes.Find(id);
            db.LoaiVeSoes.Remove(loaiVeSo);
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
