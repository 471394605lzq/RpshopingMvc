using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class YGOrder
    {
        public int ID { get; set; }
        [Display(Name = "用户id")]
        public int UserID { get; set; }
        [Display(Name = "云购期数")]
        public int IssueID { get; set; }
        [Display(Name = "订单时间")]
        public string OrderTime { get; set; }
        [Display(Name = "云购码")]
        public string LockCode { get; set; }
        [Display(Name = "购买人次")]
        public int BuyNumber { get; set; }
        [Display(Name = "付款方式")]
        public PayType Paytype { get; set; }
    }
}