using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class GoodsSortGrade
    {
        public int ID { get; set; }
        [Display(Name ="等级名称")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Display(Name = "排序")]
        public int Index { get; set; }
    }
}