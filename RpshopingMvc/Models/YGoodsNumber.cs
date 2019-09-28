using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class YGoodsIssue
    {
        public int ID { get; set; }
        //期数
        public int IssueNumber { get; set; }
        //状态
        public string State { get; set; }
        //揭晓时间
        public string AnnounceTime { get; set; }
        //购买数量
        public int AlreadyNumber { get; set; }
        //剩余数量
        public int SurplusNumber { get; set; }
        //总需数量
        public int SumNumber { get; set; }
        //幸运号
        public int LuckCode { get; set; }
        
    }
}