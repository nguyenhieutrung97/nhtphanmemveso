using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class DotPhatHanh
    {
        public int LoaiVeSoId { get; set; }
        public System.DateTime NgayPhat { get; set; }
        public List<PhieuPhatHanhVM> ListPhieuPhatHanh { get; set; } 
    }
}
