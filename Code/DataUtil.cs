using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebQL.Areas.Admin.Models;

namespace WebApplication1.Code
{
    public class DataUtil
    {
        public static SqlConnection conn = new SqlConnection();
        public static String connstr;
        public static String dataBase = "KHACHSAN";
        public static String nameServer = "DESKTOP-IEF1TTK";
        public static String loginName = "sa";
        public static string password = "hochima";
        public static string username = "";
        public static String hoten = "";
        public static string bophan = "";
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static Random random = new Random();

        public static int KetNoi()
        {
            try
            {
                GetDBConnection();
                conn.Open();
                return 1;
            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return 0;
            }
        }
        public static void setConString()
        {
            DataUtil.connstr = "Data Source=" + DataUtil.nameServer + ";Initial Catalog=" +
                  DataUtil.dataBase + ";User ID=" +
                  DataUtil.loginName + ";password=" + DataUtil.password;
        }
        public static SqlConnection GetDBConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
            setConString();
            DataUtil.conn.ConnectionString = DataUtil.connstr;
            string connString = DataUtil.connstr;
            conn = new SqlConnection(connString);
            return conn;
        }
        public static DataTable ExecSqlDataTable(String cmd, string connstr)
        {
            DataTable dt = new DataTable();
            GetDBConnection();
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public static Boolean ExecQueryString(string query)
        {
            GetDBConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            int count = cmd.ExecuteNonQuery();
            conn.Close();
            if (count == 0) return false;
            else return true;
        }
        public static Boolean ExecQueryString(string query, SqlTransaction transaction)
        {

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Transaction = transaction;
            int count = cmd.ExecuteNonQuery();
            if (count == 0) return false;
            else return true;
        }
     
        public static Boolean addPhieuDat(RentInfo r, List<Room> lstrooms)
        {
            try
            {
                //GetDBConnection();
                //conn.Open();
                //SqlTransaction transaction = conn.BeginTransaction();
                String cmndkh = r.idCard;
                String checkkh = "Select * FROM KHACHHANG WHERE CMND_PASSPORT ='" + r.idCard + "'";
                int temp = 0;
                String mahuy = RandomString();

                if (r.gender)
                    temp = 1;
                DataTable dt = ExecSqlDataTable(checkkh, DataUtil.connstr);
                if (dt.Rows.Count > 0)
                {
                    
                    String sqlkh = "UPDATE [dbo].[KHACHHANG] SET  [HoKH] = N'" + "',[TenKH] = N'" + r.nameClient + "',[SoDienThoaiKH] = '" + r.phone + "',[GioiTinhKH] = " + temp+ ",[NgaySinhKH] = '" + r.birth + "',[Email] = '" + r.email + "' WHERE CMND_PASSPORT = '" + r.idCard + "'";
                    if (ExecQueryString(sqlkh))
                    {
                        String checkippd = "select * from PHIEUDAT ";
                        DataTable table = ExecSqlDataTable(checkippd, connstr);
                        String mpd = "ONLINE" + (table.Rows.Count + 1);
                        string sql = "INSERT INTO [PHIEUDAT]([MAPhieuDat],[CNMDKH],[MANV],[NgayDen],[NgayDi],[NgayDat],[TrangThaiPhieudat],[TienCoc],[MaHuy])VALUES('" + mpd + "',N'" + r.idCard + "',NULL,'" + r.dateRent + "','" + r.datePay + "','" + DateTime.Now + "', " + 1 + "," + r.deposit + ",'"+ mahuy + "')";

                        if (ExecQueryString(sql))
                        {
                            foreach (Room room in lstrooms)
                            {
                                string sqlct = "INSERT INTO [CTPHIEUDAT]([MAPhieuDat],[MAHangPhong],[SoLuong])VALUES('" + mpd + "','" + room.hangphong + "','" + room.soluong + "')";
                                if (!ExecQueryString(sqlct))
                                {
                                    throw new System.Exception("Error:  " + mpd);
                                }
                            }
                        }
                        else
                        {
                            throw new System.Exception("Error:  " + mpd);

                        }
                    }
                }
                else
                {
                    string sqlkh = "INSERT INTO [KHACHHANG]([CMND_PASSPORT],[HoKH],[TenKH],[SoDienThoaiKH],[GioiTinhKH],[NgaySinhKH],[Email]) VALUES('" + r.idCard + "',N'" + "',N'" + r.nameClient + "','" + r.phone + "'," + temp + ",'" + r.birth + "','" + r.email + "')";
                    if (ExecQueryString(sqlkh))
                    {
                        String checkippd = "select * from PHIEUDAT ";
                        GetDBConnection();
                        conn.Open();
                        DataTable t = ExecSqlDataTable(checkippd,connstr);
                        conn.Close();
                        String mpd = "ONLINE" + (t.Rows.Count + 1);
                        string sql = "INSERT INTO [PHIEUDAT]([MAPhieuDat],[CNMDKH],[MANV],[NgayDen],[NgayDi],[NgayDat],[TrangThaiPhieuDat],[TienCoc],[MaHuy])VALUES('" + mpd + "',N'" + r.idCard + "',NULL,'" + r.dateRent + "','" + r.datePay + "','" + DateTime.Now + "', " + 1 + "," + r.deposit + ",'Ma Huy')";

                        if (ExecQueryString(sql))
                        {
                            foreach (Room room in lstrooms)
                            {
                                string sqlct = "INSERT INTO [CTPHIEUDAT]([MAPhieuDat],[MAHangPhong],[SoLuong])VALUES('" + mpd + "','" + room.hangphong + "','" + room.soluong + "')";
                                if (!ExecQueryString(sqlct))
                                {
                                    throw new System.Exception("Error:  " + mpd);
                                }
                            }
                            //transaction.Commit();
                        }
                        else
                        {
                            //transaction.Rollback();
                            throw new System.Exception("Error:  " + mpd);

                        }
                    }
                    else
                    {
                        throw new System.Exception("Error:  " + r.idCard);

                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Write("aaaaaaaaaa" + e.Message);
                return false;
            }


        }

        private static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}