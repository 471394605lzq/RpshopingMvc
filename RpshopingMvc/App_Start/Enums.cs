using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Enums
{
    public class Enums
    {
        public enum YesOrNo {
            [Display(Name = "是")]
            Yes,
            [Display(Name = "否")]
            No
        }

        /// <summary>
        /// 充值类型
        /// </summary>
        public enum RechargeType
        {
            [Display(Name = "10")]
            Ten,
            [Display(Name = "30")]
            Thirty,
            [Display(Name = "50")]
            Fifty,
            [Display(Name = "100")]
            Hundred,
            [Display(Name = "200")]
            TwoHundred,
            [Display(Name = "500")]
            FiveHundred
        }
        public enum PayType {
            [Display(Name ="微信支付")]
            wx,
            [Display(Name ="支付宝")]
            ali
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public enum OrderState
        {
            [Display(Name = "待处理")]
            UnHandle = 0,
            [Display(Name = "成功")]
            Success = 1,
            [Display(Name = "失败")]
            Failed = 2,
            [Display(Name = "已取消")]
            Canceled = 3,
        }
        /// <summary>
        /// 订单付款类型(充值：充值付款类型 订单付款：现在订单直接付款类型)
        /// </summary>
        public enum OrderType
        {
            [Display(Name = "充值")]
            Recharge,
            [Display(Name = "订单付款")]
            OrderPay
        }
    }
}