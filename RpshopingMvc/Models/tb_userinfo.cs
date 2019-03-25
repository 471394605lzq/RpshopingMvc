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