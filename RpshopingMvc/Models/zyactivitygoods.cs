using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class zyactivitygoods
    {
        public int ID { get; set; }
        public int goodsid { get; set; }
        public int activityid { get; set; }
        public int Postage { get; set; }
        public decimal acrivityprice { get; set; }
        public string remark { get; set; }
    }
}