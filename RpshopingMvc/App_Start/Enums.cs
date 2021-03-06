﻿using System;
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
            ali,
            [Display(Name = "余额")]
            yy
        }
        public enum GoodsState {
            [Display(Name = "上架")]
            sj,
            [Display(Name = "下架")]
            xj,
            [Display(Name = "收完")]
            sw
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
        /// 商品订单状态
        /// </summary>
        public enum GoodsOrderState
        {
            [Display(Name = "待付款")]
            StayPay = 0,
            [Display(Name = "待发货")]
            StaySend = 1,
            [Display(Name = "待收货")]
            StayTake = 2,
            [Display(Name = "待评价")]
            StayEvaluate = 3,
            [Display(Name = "已完成")]
            Finish = 4,
            [Display(Name = "退款中")]
            RefundIng = 5,
            [Display(Name = "退款成功")]
            RefundSucces = 6,
            [Display(Name = "取消订单")]
            Cancel = 7
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
            NoBalance = 0,
            [Display(Name = "已结算")]
            IsBalance = 1,
        }
        //用户类型
        public enum UserType
        {
            [Display(Name = "普通用户")]
            PlainUser = 0,
            [Display(Name = "高级用户用户")]
            SeniorUser = 1
        }
        //云购商品类型
        public enum YGoodsEnumType
        {
            [Display(Name = "一元")]
            One = 0,
            [Display(Name = "五元")]
            Five = 1,
            [Display(Name = "十元")]
            Ten = 2,
            [Display(Name = "百元")]
            Hundred = 3
        }
        public enum IsRecommend {
            [Display(Name = "否")]
            No,
            [Display(Name = "是")]
            Yes
        }
        public enum RedPacketType
        {
            [Display(Name = "新人红包")]
            NewUser=0,
            [Display(Name = "会员红包")]
            Member = 1,

        }
        //轮播图类别
        public enum SelideType
        {
            [Display(Name = "商品")]
            goods = 0,
            [Display(Name = "展示")]
            show = 1
        }
    }
}