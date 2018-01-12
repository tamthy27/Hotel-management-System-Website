using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebApplication1.Code;

namespace WebApplication1.Models
{
    public class HangPhong1
    {
        public string mahangphong { get; set; }
        public string hangphong { get; set; }
        public int giatien { get; set; }
        public string hinhanh { get; set; }
        public int giamgia { get; set; }
        public int giatiennow { get; set; }
        public HangPhong1(DataRow row)
        {
            this.mahangphong = row["MAhangphong"].ToString();
            this.hangphong = row["HangPhong"].ToString();
            this.hinhanh = row["hinhanh"].ToString();
            String sql = "SELECT GIAPHONG.GiaHangPhong FROM HANGPHONG join(select GIAPHONG.GiaHangPhong, GIAPHONG.MAHangPhong   from GIAPHONG join (select GIAPHONG.MAHangPhong, max(NgayApDung) as ngayapdung from GIAPHONG where GIAPHONG.NgayApDung <='" + DateTime.Today + "'group by GIAPHONG.MAHangPhong) as gp1 on GIAPHONG.MAHangPhong = gp1.MAHangPhong and GIAPHONG.NgayApDung = gp1.ngayapdung)as GIAPHONG on GIAPHONG.MAHangPhong = HANGPHONG.MAHangPhong and GIAPHONG.MAHangPhong ='" + mahangphong + "'";
            String sql2 = "select CTKM_HANGPHONG.GiamGia from KhuyenMai, HANGPHONG, CTKM_HANGPHONG where KhuyenMai.MAKhuyenMai = CTKM_HANGPHONG.MaKhuyenMai and KhuyenMai.NgayApDung <='"+DateTime.Today+ "'  and KhuyenMai.NgayKetThuc >= '" + DateTime.Today + "' and CTKM_HANGPHONG.MaHangPhong = HangPhong.MAHangPhong and HangPhong.MAHangPhong ='" + this.mahangphong+"'";
            try
            {
                this.giatien = int.Parse(((DataUtil.ExecSqlDataTable(sql, DataUtil.connstr)).Rows)[0][0].ToString());
            }
            catch
            {
                this.giatien = 0;
            }
            try
            {
                this.giamgia = int.Parse(((DataUtil.ExecSqlDataTable(sql2, DataUtil.connstr)).Rows)[0][0].ToString());
                giatiennow = giatien - (giatien * giamgia / 100);
            }
            catch
            {
                this.giamgia = 0;
                giatiennow = giatien;
            }
            }
        public HangPhong1()
        {

        }
    }
}