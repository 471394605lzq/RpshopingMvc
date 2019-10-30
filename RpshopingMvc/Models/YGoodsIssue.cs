using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class YGoodsIssue
    {
        public int ID { get; set; }
        //期数
        [Display(Name = "期数")]
        public int IssueNumber { get; set; }
        //状态  进行中 已揭晓
        [Display(Name = "状态")]
        public string State { get; set; }
        //揭晓时间
        [Display(Name = "揭晓时间")]
        public string AnnounceTime { get; set; }
        //购买数量
        [Display(Name = "购买数量")]
        public int AlreadyNumber { get; set; }
        //剩余数量
        [Display(Name = "剩余数量")]
        public int SurplusNumber { get; set; }
        //总需数量
        [Display(Name = "总需数量")]
        public int SumNumber { get; set; }
        //幸运号
        [Display(Name = "幸运号")]
        public string LuckCode { get; set; }
        //云购商品id
        public int YGoodsId { get; set; }
        [Display(Name = "是否锁住")]//0：否 1:是
        public int IsLock { get; set; }
        [Display(Name = "中奖用户id")]
        public int LuckUserID { get; set; }

    }
}