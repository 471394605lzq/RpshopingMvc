﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class PayOrder
    {
        [Display(Name ="id")]
        public int ID { get; set; }
        [Display(Name = "付款用户ID")]
        [MaxLength(50)]
        public string User_ID { get; set; }
        [Display(Name = "订单创建时间")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "订单付款时间")]
        [MaxLength(50)]
        public string PayTime { get; set; }
        [Display(Name = "预支付交易订单号")]
        [MaxLength(50)]
        public string PayPrepay_id { get; set; }
        [Display(Name = "商户订单号")]
        [MaxLength(50)]
        public string out_trade_no { get; set; }
        [Display(Name = "随机字符串")]
        [MaxLength(50)]
        public string Paynoncestr { get; set; }
        [Display(Name = "应结订单金额")]
        public decimal settlement_total_fee { get; set; }
        [Display(Name = "订单金额")]
        public decimal total_fee { get; set; }
        [Display(Name = "订单支付金额")]
        public decimal cash_fee { get; set; }
        [Display(Name = "支付用户设备号")]
        [MaxLength(50)]
        public string device_info { get; set; }
        [Display(Name = "支付用户IP")]
        [MaxLength(50)]
        public string spbill_create_ip { get; set; }
        [Display(Name = "支付签名字符串")]
        public string Sign { get; set; }
        [Display(Name = "订单支付状态")]
        public OrderState OrderState { get; set; }
        [Display(Name = "微信支付订单号")]
        [MaxLength(50)]
        public string transaction_id { get; set; }

        [Display(Name = "支付的结果")]
        /// <summary>
        /// 支付的结果
        /// </summary>
        public string PayResult { get; set; }
        [Display(Name = "订单类型")]
        public OrderType OrderType { get; set; }
        [Display(Name = "关联ID")]
        //订单类型是充值则关联充值记录ID，如果是在线支付订单则关联商品订单记录ID
        public int RelationID { get; set; }
    }
}