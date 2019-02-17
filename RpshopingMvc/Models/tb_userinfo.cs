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
        [Display(Name ="云数据库用户ID")]
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

    }
}