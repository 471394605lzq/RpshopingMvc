using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class tb_TKInfo
    {
        public int ID { get; set; }
        [Display(Name ="推广者ID")]
        [MaxLength(50)]
        public string Memberid { get; set; }
        [Display(Name = "媒体ID")]
        [MaxLength(50)]
        public string Siteid { get; set; }
        [Display(Name = "推广位ID")]
        [MaxLength(50)]
        public string Adzoneid { get; set; }
    }
}