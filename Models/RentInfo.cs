using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebQL.Areas.Admin.Models
{
    public class RentInfo
    {
        [Display(Name = "Ngày đến")]
        [Required(ErrorMessage = "Bạn phải nhập ngày đến")]
        public Nullable<DateTime> dateRent { set; get; }

        [Display(Name = "Ngày đi")]
        [Required(ErrorMessage = "Bạn phải nhập ngày đi")]
        public Nullable<DateTime> datePay { set; get; }
        public bool type { set; get; }

        [Display(Name = "Tiền cọc")]
        public int deposit { set; get; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Bạn phải nhập tên")]
        public string nameClient { set; get; }

        [Display(Name = "Nữ")]
        [Required(ErrorMessage = "Bạn phải nhập giới tính")]
        public bool gender { get; set; }

        [Display(Name = "Ngày sinh")]
        public string birth { get; set; }

        [Display(Name = "CMND")]
        [Required(ErrorMessage = "Bạn phải nhập số CMND")]
        public string idCard { set; get; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn phải nhập số điện thoại")]
        public string phone { set; get; }

        [Display(Name = "Danh sách phòng thuê")]
        public List<int> listPhong { set; get; }

        [Display(Name = "id NV")]
        public int idNV { set; get; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Bạn phải nhập email")]
        public string email { set; get; }
        [Display(Name = "Ghi chú")]
        public string description { set; get; }
    }
}