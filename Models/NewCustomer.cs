using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebQL.Areas.Admin.Models
{
    public class NewCustomer
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập")]
        public string username { set; get; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        public string password { set; get; }
        public bool isClient { set; get; }
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Bạn phải nhập họ tên")]
        public string name { set; get; }
        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Bạn phải nhập giới tính")]
        public string gender { set; get; }

        public string address { set; get; }
        [Display(Name = "CMND")]
        [Required(ErrorMessage = "Bạn phải nhập CMND")]
        public string idCard { set; get; }
        public string phone { set; get; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Bạn phải nhập email")]
        public string email { set; get; }
    }
}