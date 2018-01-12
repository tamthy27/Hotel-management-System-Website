using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebQL.Areas.Admin.Models;
using WebApplication1.Models;
using System.Data;
using WebApplication1.Code;
using WebQL.Areas.Admin.Code;

namespace WebQL.Areas.Admin.Controllers
{
    public class BookRoomController : Controller
    {
        //
        // GET: /Admin/BookRoom/

        public ActionResult Index(BookRoom broom)
        {
            if (broom.dateRent != null && broom.datePay != null && broom.datePay > broom.dateRent)
            {
                List<Room> rooms = new List<Room>();
                DataTable lstroom = DataUtil.ExecSqlDataTable("Exec [SP_GETSOLUONGPHONG] '" + broom.dateRent + "','" + broom.datePay + "', null", DataUtil.connstr);
                foreach (DataRow row in lstroom.Rows)
                {
                    if ((int)row["soluong"] > 0)
                    {
                        Room temp = new Room(row);
                        try
                        {
                            String sql = "SELECT GIAPHONG.GiaHangPhong FROM HANGPHONG join(select GIAPHONG.GiaHangPhong, GIAPHONG.MAHangPhong   from GIAPHONG join (select GIAPHONG.MAHangPhong, max(NgayApDung) as ngayapdung from GIAPHONG where GIAPHONG.NgayApDung <='"+broom.dateRent.Date+"'group by GIAPHONG.MAHangPhong) as gp1 on GIAPHONG.MAHangPhong = gp1.MAHangPhong and GIAPHONG.NgayApDung = gp1.ngayapdung)as GIAPHONG on GIAPHONG.MAHangPhong = HANGPHONG.MAHangPhong and GIAPHONG.MAHangPhong ='"+temp.hangphong+"'";
                            try
                            {
                                temp.giatien = int.Parse(((DataUtil.ExecSqlDataTable(sql, DataUtil.connstr)).Rows)[0][0].ToString());
                            }
                            catch
                            {
                                temp.giatien = 0;
                            }
                            rooms.Add(temp);

                        }
                        catch
                        {
                            temp.giatien = 0;
                            rooms.Add(temp);


                        }
                    }
                }
                if (rooms != null)
                {
                    if (rooms.Count > 0)
                    {
                        SessionHelper.SetDateSession(broom);

                        TempData["datein"] = broom.dateRent;
                        TempData["dateout"] = broom.datePay;

                        return View(rooms);
                    }
                    //TempData["message"] = "Không còn phòng trống trong khoảng thời gian quý khách chọn.\n Xin lỗi quý khách vì sự bất tiện này";
                    TempData["HomeIndexError"] = HomeIndexError.RoomEmpty;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    //TempData["message"] = "Không còn phòng trống trong khoảng thời gian quý khách chọn.\n Xin lỗi quý khách vì sự bất tiện này";
                    TempData["HomeIndexError"] = HomeIndexError.RoomEmpty;
                    return RedirectToAction("Index", "Home");
                }

                //TempData["ListRoom"] = rooms;
                //TempData["datein"] = broom.dateRent;
                //TempData["dateout"] = broom.datePay;
                //TempData["date"] = broom;
                //SessionHelper.SetDateSession(broom);
                //return RedirectToAction("Index", "BookRoom");
                //var ListRoom = TempData["ListRoom"] as List<Room>;
                //if (ListRoom != null)
                //{
                //    if (ListRoom.Count > 0)
                //    {
                //        BookRoom br = (BookRoom)TempData["date"];
                //        TempData["datein"] = br.dateRent;
                //        TempData["dateout"] = br.datePay;

                //        return View(ListRoom);
                //    }
                //    TempData["message"] = "Không còn phòng trống trong khoảng thời gian quý khách chọn.\n Xin lỗi quý khách vì sự bất tiện này";

                //    return RedirectToAction("Index", "Home");

                //}
                //else
                //{
                //    TempData["message"] = "Không còn phòng trống trong khoảng thời gian quý khách chọn.\n Xin lỗi quý khách vì sự bất tiện này";
                //    return RedirectToAction("Index", "Home");
                //}
            }
            else
            {
                //String message = "Ngày đi hoặc ngày đến không hợp lệ.Xin vui lòng nhập lại";
                //byte[] bytes = Encoding.UTF8.GetBytes(message);
                //message = Encoding.UTF8.GetString(bytes);
                //TempData["message"]=message;
                //Console.Write("Ngày đi hoặc ngày đến không hợp lệ.Xin vui lòng nhập lại");

                TempData["HomeIndexError"] = HomeIndexError.DateTimeIncorrect;
                return RedirectToAction("Index", "Home");

            }

           
        }

    }
}
