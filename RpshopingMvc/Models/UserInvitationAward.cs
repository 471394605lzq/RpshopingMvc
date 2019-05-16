using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class UserInvitationAward
    {
        public int ID { get; set; }
        [Display(Name = "用户云ID")]
        [MaxLength(50)]
        public string UserID { get; set; }
        [Display(Name = "奖励金额")]
        public decimal AwardMoney { get; set; }
        [Display(Name = "奖励时间")]
        public string CreateTime { get; set; }
        [Display(Name = "来源用户ID")]
        public string FromUserID { get; set; }
        [Display(Name = "是否可用")]
        public YesOrNo States { get; set; }
    }
    public class UserInvitationAwardView
    {
        public int ID { get; set; }
        [Display(Name = "用户云ID")]
        [MaxLength(50)]
        public string UserID { get; set; }
        [Display(Name = "奖励金额")]
        public decimal AwardMoney { get; set; }
        [Display(Name = "奖励时间")]
        public string CreateTime { get; set; }
        [Display(Name = "来源用户ID")]
        public string FromUserID { get; set; }
        [Display(Name = "是否可用")]
        public YesOrNo States { get; set; }
    }
}