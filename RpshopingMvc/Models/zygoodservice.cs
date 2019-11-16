using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class zygoodservice
    {
        public int ID { get; set; }
        [Display(Name = "服务名称")]
        public string Name { get; set; }
        [Display(Name = "排序")]
        public int Sort { get; set; }
        [Display(Name = "服务说明")]
        public string Explain { get; set; }
    }
    public class zygoodserviceshow
    {
        public int ID { get; set; }
        [Display(Name = "服务名称")]
        public string Name { get; set; }
        [Display(Name = "排序")]
        public int Sort { get; set; }
        [Display(Name = "服务说明")]
        public string Explain { get; set; }
    }
}