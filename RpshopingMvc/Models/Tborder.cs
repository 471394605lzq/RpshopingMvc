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
        [Display(Name = "云用户ID")]
        public string YUserID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "订单时间")]
        public DateTime OrderTime { get; set; }
        [Display(Name = "订单号")]
        public string OrderCode { get; set; }
        [Display(Name = "商品ID")]
        public int GoodsID { get; set; }
        [Display(Name = "商品名称")]
        public string GoodsName { get; set; }
        [Display(Name = "成交价")]
        public decimal OrderPrice { get; set; }
        [Display(Name = "结算时间")]
        public DateTime BalanceTime { get; set; }
        [Display(Name = "奖励金额")]
        public decimal RebateMoney { get; set; }
        [Display(Name = "商品图片")]
        public string GoodsImage { get; set; }
        [Display(Name = "订单状态")]
        public TbOrderState OrderState { get; set; }
    }
}