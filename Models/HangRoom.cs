using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebApplication1.Code;

namespace WebQL.Areas.Admin.Models
{
    public class Room
    {
        public int soluong { get; set; }
        public string hangphong { get; set; }
        public string hangphongname { get; set; }
        public int giatien { get; set; }
        public string hinhanh { get; set; }
        public int giamgia { get; set; }

        public Room(int soluong, string hangphong)
        {
            this.soluong = soluong;
            this.hangphong = hangphong;
        }
        public Room (DataRow row)
        {
            this.hangphong = row["MAhangphong"].ToString();
            this.soluong = (int) row["soluong"];
            String sql = "select HANGPHONG.HangPhong, HANGPHONG.hinhanh from hangphong where Hangphong.MAHangPhong ='" + this.hangphong + "'";
            this.hangphongname = ((DataUtil.ExecSqlDataTable(sql, DataUtil.connstr)).Rows)[0][0].ToString();
            this.hinhanh = ((DataUtil.ExecSqlDataTable(sql, DataUtil.connstr)).Rows)[0][1].ToString();

        }
        public Room()
        {

        }
    }
}