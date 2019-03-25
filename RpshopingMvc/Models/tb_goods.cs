using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class tb_goods
    {
        public int ID { get; set; }
        [Display(Name ="商品名称")]
        [MaxLength(200)]
        public string GoodsName { get; set; }
        [Display(Name = "商品ID")]
        [MaxLength(50)]
        public string Code { get; set; }
        [Display(Name = "选品库ID")]
        public int FavoritesID { get; set; }
        [Display(Name = "商品价格")]
        public decimal Price { get; set; }
        [Display(Name = "无线实际价格")]
        public decimal wxzkprice { get; set; }
        [Display(Name = "折扣价")]
        public decimal zkprice { get; set; }
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
        [Display(Name = "佣金说明")]
        public string BrokerageExplain { get; set; }
        [Display(Name = "卖家ID")]
        [MaxLength(50)]
        public string SellerID { get; set; }
        [Display(Name = "卖家昵称")]
        [MaxLength(50)]
        public string StoreNick { get; set; }
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
        [Display(Name = "商品类型")]
        public GoodsType GoodsType { get; set; }
        [Display(Name = "商品所在地")]
        [MaxLength(50)]
        public string provcity { get; set; }
        [Display(Name = "券后价")]
        public decimal Qhprice { get; set; }
        [Display(Name = "所属分类")]
        public string CategoryName { get; set; }
    }


    public class tb_goodsview
    {
        public int ID { get; set; }
        [Display(Name = "商品名称")]
        public string GoodsName { get; set; }
        [Display(Name = "商品价格")]
        public decimal Price { get; set; }
        [Display(Name = "无线实际价格")]
        public decimal wxzkprice { get; set; }
        [Display(Name = "折扣价")]
        public decimal zkprice { get; set; }
        [Display(Name = "商品主图链接")]
        public string ImagePath { get; set; }
        [Display(Name = "商品小图")]
        public string SmallImages { get; set; }
        [Display(Name = "商品一级类目")]
        public int GoodsSort { get; set; }
        [Display(Name = "月销量")]
        public int SalesVolume { get; set; }
        [Display(Name = "收入比例")]
        public decimal IncomeRatio { get; set; }
        [Display(Name = "佣金")]
        public decimal Brokerage { get; set; }
        [Display(Name = "卖家店铺名称")]
        [MaxLength(50)]
        public string StoreName { get; set; }
        [Display(Name = "平台类型")]
        [MaxLength(50)]
        public string PlatformType { get; set; }
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

    }
    //查询结果
    public class GoodsResultModel {
        public int ID { get; set; }
        [Display(Name = "商品名称")]
        public string GoodsName { get; set; }
        [Display(Name = "商品价格")]
        public decimal Price { get; set; }
        [Display(Name = "无线实际价格")]
        public decimal wxzkprice { get; set; }
        [Display(Name = "折扣价")]
        public decimal zkprice { get; set; }
        [Display(Name = "券后价")]
        public decimal qhprice { get; set; }
        [Display(Name = "商品主图链接")]
        public string ImagePath { get; set; }
        [Display(Name = "商品小图")]
        public string SmallImages { get; set; }
        [Display(Name = "月销量")]
        public int SalesVolume { get; set; }
        [Display(Name = "佣金")]
        public decimal Brokerage { get; set; }
        [Display(Name = "卖家店铺名称")]
        public string StoreName { get; set; }
        [Display(Name = "平台类型")]
        public string PlatformType { get; set; }
        [Display(Name = "优惠券总量")]
        public int CouponCount { get; set; }
        [Display(Name = "优惠券剩余量")]
        public int CouponSurplus { get; set; }
        [Display(Name = "优惠券面额")]
        public decimal CouponDenomination { get; set; }
        [Display(Name = "所属分类")]
        public string CategoryName { get; set; }
    }
    public class tbgoods {
        public long num_iid { get; set; }
        public string title { get; set; }
        public string pict_url { get; set; }
        public object small_images { get; set; }
        public decimal reserve_price { get; set; }
        public decimal zk_final_price { get; set; }
        public int user_type { get; set; }
        public string provcity { get; set; }
        public string item_url { get; set; }
        public string click_url { get; set; }
        public string nick { get; set; }
        public long seller_id { get; set; }
        public int volume { get; set; }
        public decimal tk_rate { get; set; }
        public decimal zk_final_price_wap { get; set; }
        public string shop_title { get; set; }
        public int status { get; set; }
        public long category { get; set; }
        public string coupon_click_url { get; set; }
        public string coupon_end_time { get; set; }
        public string coupon_info { get; set; }
        public string coupon_start_time { get; set; }
        public string coupon_total_count { get; set; }
        public string coupon_remain_count { get; set; }
    }
}