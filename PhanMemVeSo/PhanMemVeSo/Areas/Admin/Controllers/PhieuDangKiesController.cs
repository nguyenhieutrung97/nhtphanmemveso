using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EFModels;
using Model.ViewModels;

namespace PhanMemVeSo.Areas.Admin.Controllers
{
    public class PhieuDangKiesController : Controller
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();

        // GET: Admin/PhieuDangKies
        public ActionResult Index()
        {
            var phieuDangKies = db.PhieuDangKies.Include(p => p.DaiLy);
            phieuDangKies = phieuDangKies.OrderBy(m => m.DaiLyId);
            return View(phieuDangKies.ToList());
        }

        // GET: Admin/PhieuDangKies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDangKy phieuDangKy = db.PhieuDangKies.Find(id);
            if (phieuDangKy == null)
            {
                return HttpNotFound();
            }
            return View(phieuDangKy);
        }

        public ActionResult findDateandTypeVeSo()
        {
            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy");
            ViewBag.DateNow = System.DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult findDateandTypeVeSo(int DaiLyId, System.DateTime ngayDangKy)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Create", new { daiLyId=DaiLyId, ngayDangKy = ngayDangKy });
            }



            return View();
        }

        // GET: Admin/PhieuDangKies/Create
        public ActionResult Create(int daiLyId,  System.DateTime ngayDangKy)
        {
            ViewBag.DaiLyId = daiLyId;
            ViewBag.NgayDangKy = ngayDangKy;
            List<PhieuDangKyVM> listPDKVM = new List<PhieuDangKyVM>();
            foreach(var item in db.LoaiVeSoes)
            {
                PhieuDangKyVM phieuDangKyVM = new PhieuDangKyVM();
                phieuDangKyVM.LoaiVeSoId = item.LoaiVeSoId;
                phieuDangKyVM.TenTinh = item.TenTinh;
                listPDKVM.Add(phieuDangKyVM);
            }
            return View(listPDKVM.ToList());
        }

        // POST: Admin/PhieuDangKies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<PhieuDangKyVM> listPDKVM, int daiLyId, System.DateTime ngayDangKy)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in listPDKVM)
                {
                    if (item.SLDangKy != 0)
                    {
                        PhieuDangKy phieuDangKy = new PhieuDangKy();
                        phieuDangKy.LoaiVeSoId = db.LoaiVeSoes.Where(m => m.TenTinh == item.TenTinh).Select(m => m.LoaiVeSoId).FirstOrDefault();
                        phieuDangKy.DaiLyId = daiLyId;
                        phieuDangKy.NgayDangKy = ngayDangKy;
                        phieuDangKy.SLDangKy = item.SLDangKy;
                        db.PhieuDangKies.Add(phieuDangKy);
                        db.SaveChanges();
                    }
                    
                }
                return RedirectToAction("Index");
            }
            
            


            return View();
        }

        // GET: Admin/PhieuDangKies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDangKy phieuDangKy = db.PhieuDangKies.Find(id);
            if (phieuDangKy == null)
            {
                return HttpNotFound();
            }
            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy", phieuDangKy.DaiLyId);
            return View(phieuDangKy);
        }

        // POST: Admin/PhieuDangKies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhieuDangKyId,DaiLyId,NgayDangKy,SLDangKy")] PhieuDangKy phieuDangKy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuDangKy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy", phieuDangKy.DaiLyId);
            return View(phieuDangKy);
        }

        // GET: Admin/PhieuDangKies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDangKy phieuDangKy = db.PhieuDangKies.Find(id);
            if (phieuDangKy == null)
            {
                return HttpNotFound();
            }
            return View(phieuDangKy);
        }

        // POST: Admin/PhieuDangKies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuDangKy phieuDangKy = db.PhieuDangKies.Find(id);
            db.PhieuDangKies.Remove(phieuDangKy);
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
