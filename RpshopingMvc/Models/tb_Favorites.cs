using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class tb_Favorites
    {
        public int ID { get; set; }

        [Display(Name = "选品库名称")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "选品库图片地址")]
        [MaxLength(50)]
        public string ImagePath { get; set; }
        [Display(Name = "选品库简介说明")]
        [MaxLength(50)]
        public string Explain { get; set; }
    }
}