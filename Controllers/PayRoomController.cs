using Newtonsoft.Json;
using QLKS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebQL.Areas.Admin.Code;
using WebQL.Areas.Admin.Models;
using WebApplication1.Code;
using WebApplication1.Models;
using System.Data;
namespace WebQL.Areas.Admin.Controllers
{
    public class PayRoomController : Controller
    {
        public ActionResult Index()
        {
            var ListRoom = TempData["ls"] as List<Room>;
            return View(ListRoom);
        }

        public ActionResult InsertCMND()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertCMND(InsertCMND ir)
        {
            try
            {
                if (ModelState.IsValid && TempData != null)
            {
                    BookRoom date = SessionHelper.GetDateSession();
                    TempData["datein"] = date.dateRent;
                    TempData["dateout"] = date.datePay;
                    List<Room> lstroom = SessionHelper.getBookrooms();
                    int giatien = 0;
                    foreach (Room temp in lstroom)
                    {
                        giatien += temp.giatien * temp.soluong;
                    }
                    TempData["giatien"] = giatien;
                    String sql = "select * from KHACHHANG where CMND_PASSPORT='"+ir.idCard+"'";
                if (DataUtil.ExecQueryString(sql))
                {
                    BookRoom br = new BookRoom();
                    br = SessionHelper.GetDateSession();
                    var dt = DataUtil.ExecSqlDataTable(sql, DataUtil.connstr);
                  
                        if (dt.Rows.Count > 0)
                    {
                        DataRow kh = dt.Rows[0];
                        RentInfo ri = new RentInfo();
                        ri.deposit = (int)(giatien*20/100);
                        ri.dateRent = br.dateRent;
                        ri.datePay = br.datePay;
                        ri.idCard = kh[0].ToString();
                        ri.phone = kh[3].ToString();
                        ri.email = kh[6].ToString();
                        ri.nameClient = kh[1].ToString()+kh[2].ToString();
                        ri.birth = kh[5].ToString();
                        return View("CheckInfor", ri);
                    }
                    else
                    {
                           return View("CheckInfor");
                    }
                }
                else
                {
                    return View("CheckInfor");
                }
            }
            else
            {
                        return View("InsertCMND");
            }
            }
            catch
            {
                return RedirectToAction("Index", "Home");

            }
        }

        [HttpPost]
        public ActionResult RoomHasBeenOrdered( List<Room> model)
        {
            try
            {
                if (TempData != null)
                {
                    BookRoom date = SessionHelper.GetDateSession();
                    TempData["datein"] = date.dateRent;
                    TempData["dateout"] = date.datePay;
                    List<Room> lst = new List<Room>();
                    foreach (Room temp in model)
                    {
                        if (temp.soluong > 0)
                        {
                            lst.Add(temp);
                        }
                    }
                    if (lst.Count>0)
                    {
                        TempData["ListHangPhong"] = lst;
                        SessionHelper.setBookrooms(lst);
                        return View("Index");
                    }
                    else
                    {
                        TempData["HomeIndexError"] = HomeIndexError.NotSelectRoomYet;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["HomeIndexError"] = HomeIndexError.NotSelectRoomYet;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                TempData["HomeIndexError"] = HomeIndexError.NotSelectRoomYet;
                return RedirectToAction("Index", "Home");

            }
        }

        [HttpPost]
        public ActionResult Confirm(RentInfo ren)
        {
            try
            {
                if (ModelState.IsValid&&TempData != null)
                {
                    List<Room> lstroom = SessionHelper.getBookrooms();
                    if (DataUtil.addPhieuDat(ren, lstroom))
                    {
                        TempData["HomeIndexError"] = HomeIndexError.BookingRoomIsOk;
                        return RedirectToAction("Index", "Home");
                       // TempData["message"] = "Đặt phòng thành công.\nHãy liên hệ với khách sạn để xác nhận phiếu đặt và đóng phí đặt phòng!\n Thành thật cảm ơn";

                    }
                    else
                    {
                        TempData["HomeIndexError"] = HomeIndexError.BookingRoomIsFail;
                        return RedirectToAction("Index", "Home");
                        //TempData["message"] = "Đặt phòng thất bại , xin thử lại sau";

                    }

                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    //TempData["message"] = "Thông tin nhập vào không hợp lệ!";
                    //return RedirectToAction("Index", "Home");
                    BookRoom date = SessionHelper.GetDateSession();
                    TempData["datein"] = date.dateRent;
                    TempData["dateout"] = date.datePay;
                    List<Room> lstroom = SessionHelper.getBookrooms();
                    int giatien = 0;
                    foreach (Room temp in lstroom)
                    {
                        giatien += temp.giatien * temp.soluong;
                    }
                    TempData["giatien"] = giatien;
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin!");
                    return View("CheckInfor");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");

            }
        }
        public ActionResult CheckPayInformation(List<Room> model)
        {

            return View();
        }
    }
}