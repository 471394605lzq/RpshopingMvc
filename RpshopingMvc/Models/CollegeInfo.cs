using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class CollegeInfo
    {
        public int ID { get; set; }
        [Display(Name = "编号")]
        public int Number { get; set; }
        [Display(Name = "内容")]
        public string Info { get; set; }
        [Display(Name = "标题")]
        [MaxLength(50)]
        public string Title { get; set; }
        [Display(Name = "编号")]
        [MaxLength(50)]
        public string Code { get; set; }
    }

    public class CollegeInfoshow
    {
        public int ID { get; set; }
        [Display(Name = "编号")]
        public int Number { get; set; }
        [Display(Name = "内容")]
        public string Info { get; set; }
        [Display(Name = "标题")]
        [MaxLength(50)]
        public string Title { get; set; }
        [Display(Name = "编号")]
        public string Code { get; set; }
    }
}