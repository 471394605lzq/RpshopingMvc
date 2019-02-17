using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class tb_goods
    {
        public int ID { get; set; }
        [Display(Name ="商品名称")]
        [MaxLength(50)]
        public string GoodsName { get; set; }
        [Display(Name = "商品ID")]
        [MaxLength(50)]
        public string Code { get; set; }
        [Display(Name = "商品价格")]
        public decimal Price { get; set; }
        [Display(Name = "商品主图链接")]
        public string ImagePath { get; set; }
        [Display(Name = "商品小图")]
        public string SmallImages { get; set; }
        [Display(Name = "商品详情链接")]
        public string DetailPath { get; set; }
        [Display(Name = "商品一级类目")]
        public int GoodsSort { get; set; }
        [Display(Name = "淘宝客链接")]
        public string TkPath { get; set; }
        [Display(Name = "月销量")]
        public int SalesVolume { get; set; }
        [Display(Name = "收入比例")]
        public decimal IncomeRatio { get; set; }
        [Display(Name = "佣金")]
        public decimal Brokerage { get; set; }
        [Display(Name = "卖家ID")]
        [MaxLength(50)]
        public string SellerID { get; set; }
        [Display(Name = "卖家店铺名称")]
        [MaxLength(50)]
        public string StoreName { get; set; }
        [Display(Name = "卖家店铺图标")]
        [MaxLength(200)]
        public string StoreImage { get; set; }
        [Display(Name = "平台类型")]
        [MaxLength(50)]
        public string PlatformType { get; set; }
        [Display(Name = "优惠券ID")]
        [MaxLength(50)]
        public string CouponID { get; set; }
        [Display(Name = "优惠券总量")]
        public int CouponCount { get; set; }
        [Display(Name = "优惠券剩余量")]
        public int CouponSurplus { get; set; }
        [Display(Name = "优惠券面额")]
        public decimal CouponDenomination { get; set; }
        [Display(Name = "优惠券开始时间")]
        [MaxLength(50)]
        public string CouponStartTime { get; set; }
        [Display(Name = "优惠券结束时间")]
        [MaxLength(50)]
        public string CouponEndTime { get; set; }
        [Display(Name = "优惠券链接")]
        public string CouponPath { get; set; }
        [Display(Name = "优惠券推广链接")]
        public string CouponSpreadPath { get; set; }
        
    }
}