using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebQL.Areas.Admin.Models
{
    public class InsertCMND
    {
        [Required(ErrorMessage = "Yêu cầu nhập CMND")]
        public string idCard { get; set; }
    }
}