using RpshopingMvc.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class YGoods
    {
        public int ID { get; set; }
        [Display(Name = "商品名称")]
        public string GoodsName { get; set; }
        [Display(Name = "商品主图")]
        public string MainImage { get; set; }
        [Display(Name = "轮播图")]
        public string SamllImage { get; set; }
        [Display(Name = "价格")]
        public decimal Price { get; set; }
        [Display(Name = "商品类型")]
        public int Type { get; set; }
        [Display(Name = "库存")]
        public int Stock { get; set; }
        [Display(Name = "详情")]
        public string Info { get; set; }
    }

    public class YGoodsView
    {
        public int ID { get; set; }
        [Display(Name = "商品名称")]
        public string GoodsName { get; set; }
        [Display(Name = "商品主图")]
        public FileUpload MainImage { get; set; } = new FileUpload
        {
            AutoInit = true,
            Max = 5,
            Name = "MainImage",
            Server = Models.Common.UploadServer.QinQiu,
            Sortable = true,
            Type = Models.Common.FileType.Image,
        };
        [Display(Name = "轮播图")]
        public FileUpload SamllImage { get; set; } = new FileUpload
        {
            AutoInit = true,
            Max = 5,
            Name = "SamllImage",
            Server = Models.Common.UploadServer.QinQiu,
            Sortable = true,
            Type = Models.Common.FileType.Image,
        };
        [Display(Name = "价格")]
        public decimal Price { get; set; }
        [Display(Name = "商品类型")]
        public int Type { get; set; }
        [Display(Name = "库存")]
        public int Stock { get; set; }
        [Display(Name = "详情")]
        public string Info { get; set; }
    }

    public class YGoodsShow
    {
        public int ID { get; set; }
        [Display(Name = "商品名称")]
        public string GoodsName { get; set; }
        [Display(Name = "商品主图")]
        public string MainImage { get; set; }
        [Display(Name = "轮播图")]
        public string SamllImage { get; set; }
        [Display(Name = "价格")]
        public decimal Price { get; set; }
        [Display(Name = "商品类型")]
        public int Type { get; set; }
        [Display(Name = "商品类型")]
        public string TypeName { get; set; }
        [Display(Name = "库存")]
        public int Stock { get; set; }
        [Display(Name = "详情")]
        public string Info { get; set; }
    }
}