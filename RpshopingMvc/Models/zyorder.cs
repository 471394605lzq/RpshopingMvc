using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class zyorder
    {
        public int ID { get; set; }

        [Display(Name = "付款用户ID")]
        public int User_ID { get; set; }

        [Display(Name = "订单编号")]
        public string OrderCode { get; set; }

        [Display(Name = "订单创建时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "订单付款时间")]
        [MaxLength(50)]
        public string PayTime { get; set; }

        [Display(Name = "订单金额")]
        public decimal total_fee { get; set; }

        [Display(Name = "订单状态")]
        public GoodsOrderState OrderState { get; set; }

        [Display(Name = "商品id")]
        public int GoodsID { get; set; }

        [Display(Name = "商品数量")]
        public int GoodsNumber { get; set; }

        [Display(Name = "收货地址ID")]
        public int DeliveryAddressID { get; set; }

        [Display(Name = "买家留言")]
        public string BuyerLeave { get; set; }

        [Display(Name = "快递单号")]
        public string ExpressCode { get; set; }

        [Display(Name = "邮费")]
        public int Postage { get; set; }
    }
}