using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebQL.Areas.Admin.Code;
using WebQL.Areas.Admin.Models;
using System.Data;
using WebApplication1.Code;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<HangPhong1> rooms = new List<HangPhong1>();
            string sql = "Exec [SP_GETALLHANGPHONG] ";
            DataTable lstroom = DataUtil.ExecSqlDataTable(sql, DataUtil.connstr);
            foreach (DataRow row in lstroom.Rows)
            {
                HangPhong1 temp = new HangPhong1(row);
                rooms.Add(temp);
            }
            TempData["HangPhong"] = rooms;
            return View();
        }

        public ActionResult Instruction()
        {
            return View();
        }
        String myemail = "khachsanptit@gmail.com";
        String mypass = "khachsan";
        String myname = "automatic email";
        public ActionResult CancelOrder(string id)
        {
            String sql = "Select  KHACHHANG.Email, (KHACHHANG.HoKH+ KHACHHANG.TenKH)as hovaten , PHIEUDAT.MAPhieuDat, PHIEUDAT.MaHuy from PHIEUDAT, KHACHHANG where MAPhieuDat = '"+id+"' and PHIEUDAT.CNMDKH = KHACHHANG.CMND_PASSPORT";
            DataTable dt = DataUtil.ExecSqlDataTable(sql, DataUtil.connstr);
            
            String name = dt.Rows[0][1].ToString();
            String subject = "Hủy Phiếu Đặt";
            String message = "Bạn đã chọn hủy phiếu đặt có mã: "+dt.Rows[0][2].ToString()+ "<br /> <br /> <br />Mã hủy của phiếu đặt là:" + dt.Rows[0][3].ToString();
            string email = dt.Rows[0][0].ToString();
            sendmail(name, subject, message, email);
            return View();
        }
        public Boolean sendmail(String name, String subject, String body, String email)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new System.Net.Mail.MailAddress(myemail);
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(myemail, mypass);
                smtp.Host = "smtp.gmail.com";

                ////recipient
                //mail.To.Add(new MailAddress("kyoyuki05@gmail.com"));
                //mail.Subject = subject;
                //mail.IsBodyHtml = true;
                //String body1 = "Bạn  +" + name + "\n từ email : +" + email + "đã gởi lời góp ý \n" + body;
                //mail.Body = body1;
                //smtp.Timeout = 200000;
                //smtp.Send(mail);

                mail.To.Add(new MailAddress(email));
                mail.Subject =   subject;
                mail.IsBodyHtml = true;
                mail.Body = "Chào bạn "+name + " <br /> <br /> <br /> " + body+ " <br /> <br /> <br />  Đây là mail tự động vui lòng không trả lời mail này <br /> <br /> Mọi thắc mắc xin liên hệ với nhân viên để biết thêm chi tiết";
                smtp.Timeout = 200000;
                smtp.Send(mail);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        [HttpPost]
        public ActionResult CancelOrder(CancelOrderModel model)
        {
            if (ModelState.IsValid)
            {
                string sql = "Exec [SP_UPDATE_STATUS_HUY_PHIEU_DAT] '" + model.maPhieuDat + "','" + model.maHuy + "'";
                DataTable lstroom = DataUtil.ExecSqlDataTable(sql, DataUtil.connstr);
                var a = lstroom.Rows[0][0];
                if (a.ToString() == "0")
                {
                    TempData["HomeIndexError"] = HomeIndexError.CancelRoomIsFail;
                    return View();
                }
                else
                {
                    TempData["HomeIndexError"] = HomeIndexError.CancelRoomIsOk;                   
                    return RedirectToAction("Index");
                }

               
            }
            else
            {
                return View();
            }   
        }

        public ActionResult ChangeOrder()
        {
            return View();
        }

        public ActionResult OrderList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrderList(string idCard)
        {
            //get orderList of idCard
            DataTable lstOrder = DataUtil.ExecSqlDataTable("Exec [SP_DANH_SACH_PHIEU_DAT] '" + idCard + "'", DataUtil.connstr);
            List<OrderListModel> list = new List<OrderListModel>();
            foreach (DataRow item in lstOrder.Rows)
            {
                OrderListModel model = new OrderListModel
                {
                    MAPhieuDat = item[0].ToString(),
                    NgayDat = Convert.ToDateTime(item[1].ToString()),
                    NgayDen = Convert.ToDateTime(item[2].ToString()),
                    NgayDi = Convert.ToDateTime(item[3].ToString()),
                    TienCoc = Int32.Parse(item[4].ToString()),
                    TrangThaiPhieuDat = item[5].ToString(),
                    SoPhongDaDat = Int32.Parse(item[6].ToString())
                };
                list.Add(model);
            }


            return View(list);
        }

        public ActionResult OrderDetails(string id)
        {
            //get orderList of idCard
            String sql1 = "Exec [SP_CHI_TIET_PHIEU_DAT] '" + id + "'";
            DataTable lstRoom = DataUtil.ExecSqlDataTable(sql1, DataUtil.connstr);

            List<Room> list = new List<Room>();
            foreach (DataRow item in lstRoom.Rows)
            {
                Room room = new Room();
                room.soluong = Int32.Parse(item[0].ToString());
                room.hangphong = item[1].ToString();
                room.hangphongname = item[2].ToString();
                room.giamgia = Int32.Parse(item[3].ToString() == "" ? "0" : item[3].ToString());
                room.giatien = Int32.Parse(item[4].ToString());
                room.hinhanh = item[5].ToString();
                list.Add(room);
            }
            String sql = "Exec [SP_THONG_TIN_1_PHIEU_DAT] '" + id + "'";
            DataTable temp = DataUtil.ExecSqlDataTable(sql, DataUtil.connstr);
            OrderListModel model = new OrderListModel();

            foreach (DataRow dr in temp.Rows)
            {
                model = new OrderListModel
                {
                    MAPhieuDat = dr[0].ToString(),
                    NgayDat = Convert.ToDateTime(dr[1].ToString()),
                    NgayDen = Convert.ToDateTime(dr[2].ToString()),
                    NgayDi = Convert.ToDateTime(dr[3].ToString()),
                    TienCoc = Int32.Parse(dr[4].ToString()),
                    TrangThaiPhieuDat = dr[5].ToString(),
                    SoPhongDaDat = Int32.Parse(dr[6].ToString())
                };
            }
          
            ViewBag.RoomList = list;
            ViewBag.OrderInfo = model;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            String a = "test.w";
            String b = "lalal";
            List<String> strings = new List<string>();
            strings.Add(a);
            strings.Add(b);
            return View(strings);
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            return PartialView();
        }
        //
        // GET: /Admin/Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //[HttpPost]
        //public ActionResult Book(BookRoom broom)
        //{

        //}
    }
}