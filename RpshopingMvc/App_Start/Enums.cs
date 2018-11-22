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
            Ten=10,
            [Display(Name = "30")]
            Thirty=30,
            [Display(Name = "50")]
            Fifty=50,
            [Display(Name = "100")]
            Hundred=100,
            [Display(Name = "200")]
            TwoHundred=200,
            [Display(Name = "500")]
            FiveHundred=500
        }
        public enum PayType {
            [Display(Name ="微信支付")]
            wx,
            [Display(Name ="支付宝")]
            ali
        }
        //订单是否支付
        public enum PayOrderState
        {
            [Display(Name = "待支付")]
            wait,
            [Display(Name = "已支付")]
            already
        }
    }
}