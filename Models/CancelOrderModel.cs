using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CancelOrderModel
    {
        [Required]

        public string maPhieuDat { get; set; }

        [Required]

        public string maHuy { get; set; }
    }
}