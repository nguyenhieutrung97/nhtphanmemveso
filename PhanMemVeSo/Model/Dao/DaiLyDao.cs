using Model.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class DaiLyDao
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();
        public decimal TinhToanSLPhatTheoDaiLy(int loaiVeSoId,int daiLyId, System.DateTime ngayPhatHienTai)
        {
            decimal slDangKy = db.PhieuDangKies.OrderByDescending(m => m.NgayDangKy).Where(m => m.DaiLyId == daiLyId & System.DateTime.Compare(m.NgayDangKy, ngayPhatHienTai) <=0).Select(m=>m.SLDangKy).FirstOrDefault();
            System.DateTime ngayDangKy= db.PhieuDangKies.OrderByDescending(m => m.NgayDangKy).Where(m => m.DaiLyId == daiLyId &m.LoaiVeSoId==loaiVeSoId& System.DateTime.Compare(m.NgayDangKy, ngayPhatHienTai) <= 0).Select(m => m.NgayDangKy).FirstOrDefault();
            var listTop3 = db.PhieuPhatHanhs.Where(m => m.DaiLyId == daiLyId & System.DateTime.Compare(m.NgayPhat, ngayPhatHienTai) <= 0 &m.SLBanDuoc!=null).OrderByDescending(m => m.NgayPhat).Take(3);
            int count = listTop3.Count();
            if (count == 0)
            {
                return slDangKy;
            }
            else {
                decimal? sum = 0;
                foreach(var item in listTop3)
                {
                    sum += item.SLBanDuoc / item.SLPhat;
                }
                decimal? getReturn = Math.Round((decimal)sum / count * slDangKy);
                return getReturn??default(decimal);
            }
        }
    }
}
