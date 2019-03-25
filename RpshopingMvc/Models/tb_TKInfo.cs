using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

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
        [Display(Name = "PID")]
        public string PID { get; set; }
        [Display(Name = "是否绑定")]
        public YesOrNo PIDState { get; set; }
        public int UID { get; set; }
    }
}