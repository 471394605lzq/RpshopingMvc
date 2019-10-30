using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    //积分明细
    public class IntegralDetails
    {
        public int ID { get; set; }
        [Display(Name = "用户id")]
        public int UserID { get; set; }
        [Display(Name = "积分数量")]
        public int IntegralNumber { get; set; }
        [Display(Name = "添加时间")]
        public string AddTime { get; set; }
        [Display(Name = "操作方式")]
        public string AddType { get; set; }
        [Display(Name = "增加或者扣除")]//0：添加 1：扣除
        public int AddOrReduce { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}