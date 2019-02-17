using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class tb_goodssort
    {
        public int ID { get; set; }
        [Display(Name ="分类名称")]
        [MaxLength(50)]
        public string SortName { get; set; }
        [Display(Name ="排序")]
        public int SortIndex { get; set; }
        [Display(Name ="分类等级")]
        public int Grade { get; set; }
        [Display(Name ="上级分类ID")]
        public int ParentID { get; set; }
        [Display(Name ="分类图片地址")]
        [MaxLength(200)]
        public string ImagePath { get; set; }
    }
}