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

        /// <summary>
        /// 设备类型
        /// </summary>
        [Flags]
        public enum DriveType
        {
            Windows = 1,
            IPhone = 2,
            IPad = 4,
            Android = 8,
            WindowsPhone = 16,
        }

        public enum DebugLog
        {
            /// <summary>
            /// 所有
            /// </summary>
            All,
            /// <summary>
            /// 不输出
            /// </summary>
            No,
            /// <summary>
            /// 警告以上
            /// </summary>
            Warning,
            /// <summary>
            /// 错误以上
            /// </summary>
            Error
        }

        public enum DebugLogLevel
        {
            /// <summary>
            /// 普通记录
            /// </summary>
            Normal,
            /// <summary>
            /// 警告级别
            /// </summary>
            Warning,
            /// <summary>
            /// 错误级别
            /// </summary>
            Error
        }

        /// <summary>
        /// 占位图
        /// </summary>
        public enum DummyImage
        {
            [Display(Name = "默认")]
            Default,
            [Display(Name = "头像")]
            Avatar
        }

        public enum ResizerMode
        {
            Pad,
            Crop,
            Max,
        }

        public enum ReszieScale
        {
            Down,
            Both,
            Canvas
        }
        /// <summary>
        /// 错误页面的Layout类别，给错误页面使用的一个枚举
        /// </summary>
        public enum Layout
        {
            Manage,
            WebSide,
            MoblieWebSide
        }
        //选品库类型
        public enum FavoritesType {
            [Display(Name = "普通")]
            Ordinary = 0,
            [Display(Name = "推荐")]
            Recommend = 1,
        }

        public enum GoodsType {
            [Display(Name = "普通")]
            Ordinary = 0,
            [Display(Name = "推荐")]
            Synchronous = 1,
        }

        //用户等级类型
        public enum UserGrade
        {
            [Display(Name = "初级会员")]
            Primary = 0,
            [Display(Name = "高级会员")]
            Senior = 1,
            [Display(Name = "运营商")]
            Operator = 2,
            [Display(Name = "合伙人")]
            Partner = 3
        }
        //订单状态
        public enum TbOrderState {
            [Display(Name = "待结算")]
            IsBalance = 0,
            [Display(Name = "已结算")]
            NoBalance = 1,
        }
    }
}