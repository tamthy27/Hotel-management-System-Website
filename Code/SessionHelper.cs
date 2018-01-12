using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebQL.Areas.Admin.Models;
using WebApplication1.Models;

namespace WebQL.Areas.Admin.Code
{
    public class SessionHelper
    {
        public static void SetSession(KhachHang session)
        {
            HttpContext.Current.Session["loginSession"] = session.username;
            HttpContext.Current.Session["HoTen"] = session.HoTen;
            HttpContext.Current.Session["GioiTinh"] = session.GioiTinh;
            HttpContext.Current.Session["DC"] = session.DC;
            HttpContext.Current.Session["CMND"] = session.CMND;
            HttpContext.Current.Session["DT"] = session.DT;
            HttpContext.Current.Session["Email"] = session.Email;

        }
        public static KhachHang GetKHSession()
        {
            KhachHang session = new KhachHang();
            session.username = HttpContext.Current.Session["loginSession"] as string;
            session.HoTen = HttpContext.Current.Session["HoTen"] as string;
            session.GioiTinh = HttpContext.Current.Session["GioiTinh"] as string;
            session.DC = HttpContext.Current.Session["DC"] as string;
            session.CMND = HttpContext.Current.Session["CMND"] as string;
            session.DT = HttpContext.Current.Session["DT"] as string;
            session.Email = HttpContext.Current.Session["Email"] as string;
            if (session.username == null)
            {
                return null;
            }
            else
            {
                return session as KhachHang;
            }
        }

        public static void SetDateSession(BookRoom bookDate)
        {
            HttpContext.Current.Session["PickupDate"] = bookDate.dateRent;
            HttpContext.Current.Session["ReturnDate"] = bookDate.datePay;

        }
        public static String GetSession()
        {
            var session = HttpContext.Current.Session["loginSession"];
            if (session == null)
            {
                return null;
            }
            else
            {
                return session as String;
            }
        }


        public static BookRoom GetDateSession()
        {
            var PickupDate = HttpContext.Current.Session["PickupDate"];
            var ReturnDate = HttpContext.Current.Session["ReturnDate"];
            if (PickupDate == null && ReturnDate == null)
            {
                return null;
            }
            else
            {
                BookRoom r = new BookRoom();
                r.dateRent = (Convert.ToDateTime(PickupDate));
                r.datePay = (Convert.ToDateTime(ReturnDate));
                return r;
            }
        }
        public static void setBookrooms(List<Room>rooms)
        {
            HttpContext.Current.Session["ListRoom"] = rooms;

        }
        public static List<Room> getBookrooms()
        {
            var temp = HttpContext.Current.Session["ListRoom"];
            List<Room> rooms = (List<Room>)temp;
            return rooms;
        }
    }
}