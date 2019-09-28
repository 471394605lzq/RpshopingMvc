using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    //云购产品类别
    public class YGoodsType
    {
        public int ID { get; set; }
        //类别名称
        [Display(Name = "商品名称")]
        [Required]
        public string Name { get; set; }

    }
    public class YGoodsTypeView
    {
        public int ID { get; set; }
        //类别名称
        [Display(Name = "商品名称")]        
        public string Name { get; set; }

    }
}