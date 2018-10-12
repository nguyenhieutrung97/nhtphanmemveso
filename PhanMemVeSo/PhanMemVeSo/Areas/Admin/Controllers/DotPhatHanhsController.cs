using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EFModels;
using Model.ViewModels;
using Model.Dao;
using System.Globalization;
using System.Net;
using System.Data.Entity;

namespace PhanMemVeSo.Areas.Admin.Controllers
{
    public class DotPhatHanhsController : Controller
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();
        // GET: Admin/DotPhatHanhs
        public ActionResult Index(Nullable<DateTime> searchDate)
        {
            DateTime ngayPhatGanNhat = db.PhieuPhatHanhs.OrderByDescending(m => m.NgayPhat).Select(m => m.NgayPhat).FirstOrDefault();
            ViewBag.NgayPhatGanNhat = ngayPhatGanNhat;

            var listPhatHanh = db.PhieuPhatHanhs.ToList();
            
            String s = null;
            if (searchDate != null)
            {
                listPhatHanh=db.PhieuPhatHanhs.Where(m=>m.NgayPhat==searchDate).ToList();
                DateTime searchDate1 = searchDate ?? default(DateTime);
                DateTime d = searchDate1;
                s = d.ToString("yyyy-MM-dd");
            }
            else
            {
                s = ngayPhatGanNhat.ToString("yyyy-MM-dd");
            }
            
            ViewBag.SearchDate = s;
            return View(listPhatHanh);
        }

        // GET: Admin/PhieuPhatHanhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhatHanh phieuPhatHanh = db.PhieuPhatHanhs.Find(id);
            if (phieuPhatHanh == null)
            {
                return HttpNotFound();
            }
            return View(phieuPhatHanh);
        }

        public ActionResult findDateandTypeVeSo()
        {
            ViewBag.LoaiVeSoId = new SelectList(db.LoaiVeSoes, "LoaiVeSoId", "TenTinh");
            ViewBag.DateNow = System.DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult findDateandTypeVeSo(int LoaiVeSoId,System.DateTime ngayPhatHanh)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Create",new { loaiVeSoId =LoaiVeSoId, ngayPhat =ngayPhatHanh});
            }



            return View();
        }

        // GET: Admin/PhieuPhatHanhs/Create
        public ActionResult Create(int loaiVeSoId ,System.DateTime ngayPhat)
        {

            ViewBag.LoaiVeSo = db.LoaiVeSoes.Where(m=>m.LoaiVeSoId==loaiVeSoId).Select(m=>m.TenTinh).SingleOrDefault();
            ViewBag.NgayPhatHanh = ngayPhat;

            DotPhatHanh dotPhatHanh = new DotPhatHanh();
            dotPhatHanh.NgayPhat = ngayPhat;
            dotPhatHanh.LoaiVeSoId = loaiVeSoId;
            List<PhieuPhatHanhVM> listPhieuPhatHanh = new List<PhieuPhatHanhVM>();
            var listDaiLyId = db.DaiLies.Select(m => m.DaiLyId).ToList();
            foreach(var item in listDaiLyId)
            {
                PhieuPhatHanhVM phieuPhatHanhVM = new PhieuPhatHanhVM();
                phieuPhatHanhVM.DaiLyId = item;
                DaiLyDao daiLyDao = new DaiLyDao();
                phieuPhatHanhVM.SLPhat=daiLyDao.TinhToanSLPhatTheoDaiLy(loaiVeSoId,item, ngayPhat   );
                phieuPhatHanhVM.TenDaiLy = db.DaiLies.Where(m => m.DaiLyId == item).Select(m => m.TenDaiLy).SingleOrDefault();
                listPhieuPhatHanh.Add(phieuPhatHanhVM);
            }
            dotPhatHanh.ListPhieuPhatHanh = listPhieuPhatHanh;
            
            return View(dotPhatHanh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( DotPhatHanh dotPhatHanh,System.DateTime ngayPhat,int loaiVeSoId)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in dotPhatHanh.ListPhieuPhatHanh)
                {
                    PhieuPhatHanh phieuPhatHanh = new PhieuPhatHanh();
                    phieuPhatHanh.LoaiVeSoId = loaiVeSoId;
                    phieuPhatHanh.NgayPhat = ngayPhat;
                    phieuPhatHanh.DaiLyId = db.DaiLies.Where(m=>m.TenDaiLy==item.TenDaiLy).Select(m=>m.DaiLyId).FirstOrDefault();
                    phieuPhatHanh.SLPhat = item.SLPhat;
                    db.PhieuPhatHanhs.Add(phieuPhatHanh);
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(dotPhatHanh);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhatHanh phieuPhatHanh = db.PhieuPhatHanhs.Find(id);
            if (phieuPhatHanh == null)
            {
                return HttpNotFound();
            }
            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy", phieuPhatHanh.DaiLyId);
            ViewBag.LoaiVeSoId = new SelectList(db.LoaiVeSoes, "LoaiVeSoId", "TenTinh", phieuPhatHanh.LoaiVeSoId);
            return View(phieuPhatHanh);
        }

        // POST: Admin/PhieuPhatHanhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhieuPhatHanhId,DaiLyId,LoaiVeSoId,NgayPhat,SLPhat,SLBanDuoc")] PhieuPhatHanh phieuPhatHanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuPhatHanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy", phieuPhatHanh.DaiLyId);
            ViewBag.LoaiVeSoId = new SelectList(db.LoaiVeSoes, "LoaiVeSoId", "TenTinh", phieuPhatHanh.LoaiVeSoId);
            return View(phieuPhatHanh);
        }
        // GET: Admin/PhieuPhatHanhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhatHanh phieuPhatHanh = db.PhieuPhatHanhs.Find(id);
            if (phieuPhatHanh == null)
            {
                return HttpNotFound();
            }
            return View(phieuPhatHanh);
        }

        // POST: Admin/PhieuPhatHanhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuPhatHanh phieuPhatHanh = db.PhieuPhatHanhs.Find(id);
            db.PhieuPhatHanhs.Remove(phieuPhatHanh);
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