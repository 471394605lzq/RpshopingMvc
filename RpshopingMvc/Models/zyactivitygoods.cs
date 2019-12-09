using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class zyactivitygoods
    {
        public int ID { get; set; }
        [Display(Name = "产品id")]
        public int goodsid { get; set; }
        [Display(Name = "活动id")]
        public int activityid { get; set; }
        [Display(Name = "邮费")]
        public int Postage { get; set; }
        [Display(Name = "活动价格")]
        public decimal acrivityprice { get; set; }
        [Display(Name = "备注说明")]
        public string remark { get; set; }
        [Display(Name = "活动数量")]
        public int activenumber { get; set; }
    }
}