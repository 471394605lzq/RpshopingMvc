using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class Tborder
    {
        public int ID { get; set; }
        [Display(Name = "订单时间")]
        public DateTime OrderTime { get; set; }
        [Display(Name = "订单号")]
        public string OrderCode { get; set; }
        [Display(Name = "商品ID")]
        public string GoodsID { get; set; }
        [Display(Name = "商品名称")]
        public string GoodsName { get; set; }
        [Display(Name = "成交价")]
        public decimal OrderPrice { get; set; }
        [Display(Name = "商品图片")]
        public string GoodsImage { get; set; }
        [Display(Name = "订单状态")]
        public TbOrderState OrderState { get; set; }
        [Display(Name = "店铺名称")]
        public string StoreName { get; set; }
        [Display(Name = "收入比率")]
        public decimal IncomeRate { get; set; }
        [Display(Name = "订单类型")]
        public string OrderType { get; set; }
        [Display(Name = "商品类型")]
        public decimal GoodsPrice { get; set; }
        [Display(Name = "预估收入")]
        public decimal EstimateIncome { get; set; }
        [Display(Name = "结算金额")]
        public decimal SettlementMoney { get; set; }
        [Display(Name = "效果预估")]
        public decimal EffectIncome { get; set; }
        [Display(Name = "结算时间")]
        public DateTime SettlementTime { get; set; }
        [Display(Name = "广告位ID")]
        public string AdsenseID { get; set; }


    }
}

//[{"创建时间":"2019-03-21 09:25:25","点击时间":"2019-03-21 09:24:14","商品信息":"4条装日系内裤女纯棉高腰大码女士三角裤全棉质面料少女蕾丝短裤",
//"商品ID":"541009215405","掌柜旺旺":"wisdomfeng","所属店铺":"美多拉内衣","商品数":"1","商品单价":"76.00","订单状态":"订单付款","订单类型":"淘宝","收入比率":"9.00 %",
//"分成比率":"100.00 %","付款金额":"37.80","效果预估":"3.40","结算金额":"0.00","预估收入":"0.00","结算时间":"","佣金比率":"9.00 %","佣金金额":"0.00","技术服务费比率":"",
//"补贴比率":"0.00 %","补贴金额":"0.00","补贴类型":"-","成交平台":"无线","第三方服务来源":"--","订单编号":"384487585332201957","类目名称":"内衣/家居服","来源媒体ID":"283700162","来源媒体名称":"rp云购",
//"广告位ID":"96815000311","广告位名称":"001"}]