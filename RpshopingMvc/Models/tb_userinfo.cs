using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class tb_userinfo
    {
        public int ID { get; set; }
        [Display(Name = "云数据库用户ID")]
        [MaxLength(50)]
        public string UserID { get; set; }
        [Display(Name = "余额")]
        public decimal Balance { get; set; }
        [Display(Name = "积分")]
        public int Integral { get; set; }
        [Display(Name = "是否首充")]
        public YesOrNo FirstCharge { get; set; }
        [Display(Name = "奖励金额")]
        public decimal RewardMoney { get; set; }
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Display(Name = "手机号")]
        [MaxLength(50)]
        public string Phone { get; set; }
        [Display(Name = "用户等级类型")]
        public UserGrade UserGrade { get; set; }
        [Display(Name = "上级ID")]
        public int ParentID { get; set; }


        //PID说明mm_memberid_siteid_adzoneid
        [Display(Name = "推广者ID")]
        [MaxLength(50)]
        public string Memberid { get; set; }
        [Display(Name = "媒体ID")]
        [MaxLength(50)]
        public string Siteid { get; set; }
        [Display(Name = "推广位ID")]
        [MaxLength(50)]
        public string Adzoneid { get; set; }
        [Display(Name = "pid")]
        [MaxLength(100)]
        public string PID { get; set; }
        [Display(Name = "用户编号")]
        [MaxLength(50)]
        public string UserCode { get; set; }
        [Display(Name = "本月预估收入")]
        public decimal ThisMonthEstimateIncome { get; set; }
        [Display(Name = "本月待结算")]
        public decimal ThisMonthSettlementMoney { get; set; }

        [Display(Name = "上月预估收入")]
        public decimal LastMonthEstimateIncome { get; set; }
        [Display(Name = "上月待结算")]
        public decimal LastMonthSettlementMoney { get; set; }
        [Display(Name = "用户头像")]
        public string UserImage { get; set; }
        [Display(Name = "支付宝账号")]
        [MaxLength(50)]
        public string AliAccount { get; set; }
        [Display(Name = "支付宝实名")]
        [MaxLength(50)]
        public string AliUserName { get; set; }
        [Display(Name = "密码")]
        [MaxLength(50)]
        public string UsPwd { get; set; }
        [Display(Name = "openid")]
        [MaxLength(50)]
        public string OpenID { get; set; }
        [Display(Name = "性别")]
        [MaxLength(50)]
        public string sex { get; set; }
        [Display(Name = "生日")]
        [MaxLength(50)]
        public string birthday { get; set; }
        [Display(Name = "家乡")]
        [MaxLength(50)]
        public string hometown { get; set; }
        [Display(Name = "居住地")]
        [MaxLength(50)]
        public string residence { get; set; }
        [Display(Name = "签名")]
        [MaxLength(50)]
        public string signature { get; set; }
        [Display(Name = "用户二维码")]
        public string UserPath { get; set; }
        [Display(Name = "淘宝用户id")]
        [MaxLength(50)]
        public string tbuserid { get; set; }
        public string createtime { get; set; }
        [Display(Name = "用户类型")]
        public UserType UserType { get; set; }
    }
    public class userinfoview {
         public int ID { get; set; }
        [Display(Name ="云数据库用户ID")]
        public string UserID { get; set; }
        [Display(Name = "余额")]
        public decimal Balance { get; set; }
        [Display(Name = "积分")]
        public int Integral { get; set; }
        [Display(Name = "是否首充")]
        public YesOrNo FirstCharge { get; set; }
        [Display(Name = "奖励金额")]
        public decimal RewardMoney { get; set; }
    }
}