using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class goodstypetemp
    {
        public int ID { get; set; }
        [Display(Name = "商品分类id")]
        public int goodstypeid { get; set; }
        [Display(Name = "商品id")]
        public int goodsid { get; set; }
    }
}