using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderListModel
    {
        public string MAPhieuDat { get; set; }
        public int SoPhongDaDat{ get; set; }
        public int TienCoc { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayDen { get; set; }
        public DateTime NgayDi { get; set; }
        public string TrangThaiPhieuDat { get; set; }


    }

}