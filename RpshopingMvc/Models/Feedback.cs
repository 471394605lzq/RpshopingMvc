using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class Feedback
    {
        public int ID { get; set; }
        [Display(Name = "反馈用户ID")]
        public string UserID { get; set; }
        [Display(Name = "标题")]
        [MaxLength(50)]
        public string Titlestr { get; set; }
        [Display(Name = "反馈内容")]
        public string Contentstr { get; set; }
        [Display(Name = "联系方式")]
        [MaxLength(50)]
        public string Contactway { get; set; }
        [Display(Name = "反馈时间")]
        [MaxLength(50)]
        public string BackTime { get; set; }
    }
}