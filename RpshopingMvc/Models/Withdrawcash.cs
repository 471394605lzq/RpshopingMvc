using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class Withdrawcash
    {
        public int ID { get; set; }
        [Display(Name = "out_biz_no")]
        [MaxLength(50)]
        public string out_biz_no { get; set; }
        [Display(Name = "支付宝订单号")]
        [MaxLength(50)]
        public string order_id { get; set; }
        [Display(Name = "付款时间")]
        [MaxLength(50)]
        public string pay_date { get; set; }
        [Display(Name = "用户id")]
        public int Userid { get; set; }
        [Display(Name = "提现账号")]
        [MaxLength(50)]
        public string AliAccount { get; set; }
        [Display(Name = "提现用户")]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Display(Name = "提现金额")]
        public decimal txamount { get; set; }
        [Display(Name = "提现月份")]
        [MaxLength(50)]
        public string txmonth { get; set; }
        [Display(Name = "支付宝签名")]
        public string signstr { get; set; }
    }
}