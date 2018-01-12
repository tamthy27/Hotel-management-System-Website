using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace WebQL.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        String myemail = "khachsanptit@gmail.com";
        String mypass = "khachsan";
        String myname = "automatic email";
        //
        // GET: /Admin/Contract/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Contract/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Contract/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Contract/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Contract/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Contract/Edit/5

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
        // GET: /Admin/Contract/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Contract/Delete/5

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

        [HttpPost]
        public ActionResult SendEmail(FormCollection collection)
        {
            try
            {
                String name= collection["contact_name"];
                String email= collection["contact_email"];
                String subject= collection["contact_subject"]; 
                String message= collection["contact_message"];
                // TODO: Add delete logic here
                if (sendmail(name,subject,message,email)) ;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public Boolean sendmail(String name,String subject, String body,String email)
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

                //recipient
                mail.To.Add(new MailAddress("kyoyuki05@gmail.com"));
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                String body1 = "Bạn  +"+ name + "\n từ email : +"+email+"đã gởi lời góp ý \n" + body ;
                mail.Body = body1;
                smtp.Timeout = 200000;
                smtp.Send(mail);

                mail.To.Add(new MailAddress(email));
                mail.Subject = "RE"+subject;
                mail.IsBodyHtml = true;
                mail.Body = "Cảm ơn bạn đã góp ý";
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
    }
}
