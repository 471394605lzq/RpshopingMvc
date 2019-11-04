using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class BalanceDetail
    {
        public int ID { get; set; }
        [Display(Name = "用户id")]
        public int UserID { get; set; }
        [Display(Name = "结余金额")]
        public decimal CountBalance { get; set; }
        [Display(Name = "消耗金额")]
        public decimal MoverBalance { get; set; }
        [Display(Name = "消耗类型")]//0：增加 1：扣除
        public int MoverType { get; set; }
        [Display(Name = "消耗时间")]
        public DateTime MoverTime { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}