using RpshopingMvc.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class goods
    {
        public int ID { get; set; }
        [Display(Name = "商品名称")]
        [MaxLength(200)]
        public string GoodsName { get; set; }
        [Display(Name = "商品编号")]
        [MaxLength(50)]
        public string Code { get; set; }
        [Display(Name = "商品价格")]
        public decimal Price { get; set; }
        [Display(Name = "折扣价")]
        public decimal zkprice { get; set; }
        [Display(Name = "商品主图链接")]
        public string ImagePath { get; set; }
        [Display(Name = "商品小图")]
        public string SmallImages { get; set; }
        [Display(Name = "商品详情")]
        public string DetailPath { get; set; }
        [Display(Name = "销量")]
        public int SalesVolume { get; set; }
        [Display(Name = "收入比例")]
        public decimal IncomeRatio { get; set; }
        [Display(Name = "佣金")]
        public decimal Brokerage { get; set; }
        [Display(Name = "佣金说明")]
        public string BrokerageExplain { get; set; }
        [Display(Name = "邮费")]
        public int Postage { get; set; }
        [Display(Name = "库存")]
        public int Stock { get; set; }
        [Display(Name = "排序")]
        public int ByIndex { get; set; }
        [Display(Name = "状态")]
        public GoodsState GoodsState { get; set; }
        [Display(Name = "品牌")]
        public int Brand { get; set; }
        [Display(Name = "是否推荐")]
        public IsRecommend IsRecommend { get; set; }
        [Display(Name = "货源链接")]
        public string GetPath { get; set; }
        [Display(Name = "规格")]
        public string Specs { get; set; }
        [Display(Name = "发货地")]
        public string SendAddress { get; set; }
    }

    public class goodsview
    {
        public int ID { get; set; }
        [Display(Name = "商品名称")]
        public string GoodsName { get; set; }
        [Display(Name = "商品编号")]
        [MaxLength(50)]
        public string Code { get; set; }
        [Display(Name = "商品价格")]
        public decimal Price { get; set; }
        [Display(Name = "折扣价")]
        public decimal zkprice { get; set; }
        [Display(Name = "商品主图链接")]
        public FileUpload ImagePath { get; set; } = new FileUpload
        {
            AutoInit = true,
            Max = 5,
            Name = "MainImage",
            Server = Models.Common.UploadServer.QinQiu,
            Sortable = true,
            Type = Models.Common.FileType.Image,
        };
        [Display(Name = "轮播图")]
        public FileUpload SamllImages { get; set; } = new FileUpload
        {
            AutoInit = true,
            Max = 5,
            Name = "SamllImage",
            Server = Models.Common.UploadServer.QinQiu,
            Sortable = true,
            Type = Models.Common.FileType.Image,
        };

        [Display(Name = "商品分类")]
        public int GoodsType { get; set; }
        [Display(Name = "月销量")]
        public int SalesVolume { get; set; }
        [Display(Name = "收入比例")]
        public decimal IncomeRatio { get; set; }
        [Display(Name = "佣金")]
        public decimal Brokerage { get; set; }
        [Display(Name = "佣金说明")]
        public string BrokerageExplain { get; set; }
        [Display(Name = "邮费")]
        public int Postage { get; set; }
        [Display(Name = "商品详情")]
        public string DetailPath { get; set; }
        [Display(Name = "库存")]
        public int Stock { get; set; }
        [Display(Name = "排序")]
        public int ByIndex { get; set; }
        [Display(Name = "状态")]
        public GoodsState GoodsState { get; set; }
        [Display(Name = "品牌")]
        public int Brand { get; set; }
        [Display(Name = "是否推荐")]
        public IsRecommend IsRecommend { get; set; }
        [Display(Name = "货源链接")]
        public string GetPath { get; set; }
        [Display(Name = "规格")]
        public string Specs { get; set; }
        [Display(Name = "发货地")]
        public string SendAddress { get; set; }
    }

    public class goodsshow
    {
        [Display(Name = "商品编号")]
        public int ID { get; set; }
        [Display(Name = "商品名称")]
        [MaxLength(200)]
        public string GoodsName { get; set; }
        [Display(Name = "商品编号")]
        [MaxLength(50)]
        public string Code { get; set; }
        [Display(Name = "商品价格")]
        public decimal Price { get; set; }
        [Display(Name = "折扣价")]
        public decimal zkprice { get; set; }
        [Display(Name = "商品主图")]
        public string ImagePath { get; set; }
        [Display(Name = "商品小图")]
        public string SmallImages { get; set; }
        [Display(Name = "商品详情")]
        public string DetailPath { get; set; }
        [Display(Name = "商品分类")]
        public int GoodsType { get; set; }
        [Display(Name = "月销量")]
        public int SalesVolume { get; set; }
        [Display(Name = "佣金比例")]
        public decimal IncomeRatio { get; set; }
        [Display(Name = "佣金")]
        public decimal Brokerage { get; set; }
        [Display(Name = "佣金说明")]
        public string BrokerageExplain { get; set; }
        [Display(Name = "邮费")]
        public int Postage { get; set; }
        [Display(Name = "库存")]
        public int Stock { get; set; }
        [Display(Name = "排序")]
        public int ByIndex { get; set; }
        [Display(Name = "状态")]
        public GoodsState GoodsState { get; set; }
        [Display(Name = "品牌")]
        public int Brand { get; set; }
        [Display(Name = "是否推荐")]
        public IsRecommend IsRecommend { get; set; }
        [Display(Name = "货源链接")]
        public string GetPath { get; set; }
        [Display(Name = "规格")]
        public string Specs { get; set; }
        [Display(Name = "发货地")]
        public string SendAddress { get; set; }
    }

    public class activegoods
    {
        public int ID { get; set; }
        [Display(Name = "商品名称")]
        [MaxLength(200)]
        public string GoodsName { get; set; }
        [Display(Name = "商品编号")]
        [MaxLength(50)]
        public string Code { get; set; }
        [Display(Name = "商品价格")]
        public decimal Price { get; set; }
        [Display(Name = "折扣价")]
        public decimal zkprice { get; set; }
        [Display(Name = "商品主图链接")]
        public string ImagePath { get; set; }
        [Display(Name = "商品小图")]
        public string SmallImages { get; set; }
        [Display(Name = "商品详情")]
        public string DetailPath { get; set; }
        [Display(Name = "销量")]
        public int SalesVolume { get; set; }
        [Display(Name = "收入比例")]
        public decimal IncomeRatio { get; set; }
        [Display(Name = "佣金")]
        public decimal Brokerage { get; set; }
        [Display(Name = "佣金说明")]
        public string BrokerageExplain { get; set; }
        [Display(Name = "邮费")]
        public int Postage { get; set; }
        [Display(Name = "库存")]
        public int Stock { get; set; }
        [Display(Name = "排序")]
        public int ByIndex { get; set; }
        [Display(Name = "状态")]
        public GoodsState GoodsState { get; set; }
        [Display(Name = "品牌")]
        public int Brand { get; set; }
        [Display(Name = "是否推荐")]
        public IsRecommend IsRecommend { get; set; }
        [Display(Name = "货源链接")]
        public string GetPath { get; set; }
        [Display(Name = "规格")]
        public string Specs { get; set; }
        [Display(Name = "发货地")]
        public string SendAddress { get; set; }
        [Display(Name = "活动数量")]
        public int activenumber { get; set; }
        [Display(Name = "活动数量")]
        public int surplusnumber { get; set; }
    }
}