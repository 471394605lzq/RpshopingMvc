using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class tb_Recharge
    {
        [Display(Name = "id")]
        public int ID { get; set; }
        [Display(Name = "充值金额")]
        public decimal R_Money { get; set; }
        [Display(Name = "云数据库用户ID")]
        [MaxLength(50)]
        public string UserID { get; set; }
        [Display(Name = "本地数据库用户ID")]
        public int U_ID { get; set; }
        [Display(Name = "付款方式")]
        public PayType paytype { get; set; }
        [Display(Name = "赠送金额")]
        public decimal give { get; set; }
        [Display(Name = "付款时间")]
        public DateTime CreateDateTime { get; set; }
        [Display(Name = "付款类型")]
        public RechargeType RechargeType { get; set; }
        public int PayOrderID { get; set; }
    }
}