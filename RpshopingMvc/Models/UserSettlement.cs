using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class UserSettlement
    {
        public int ID { get; set; }
        [Display(Name = "预估收入")]
        public decimal EstimateIncome { get; set; }
        [Display(Name = "结算金额")]
        public decimal SettlementMoney { get; set; }
        [Display(Name = "结算时间")]
        public DateTime SettlementTime { get; set; }
        [Display(Name = "用户id")]
        public int UserID { get; set; }
        [Display(Name = "订单时间")]
        public DateTime OrderTime { get; set; }
        [Display(Name = "来源用户推广位id")]
        public string FromUserAdzoneid { get; set; }
        [Display(Name = "结算比率")]
        public decimal SettlementRate { get; set; }
        [Display(Name = "计算金额")]
        public decimal BalanceMoney { get; set; }
        [Display(Name = "订单号")]
        public string OrderCode { get; set; }
        [Display(Name = "结算状态")]
        public TbOrderState SettlementState { get; set; }
    }
}