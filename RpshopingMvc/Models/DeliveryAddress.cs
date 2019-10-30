using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class DeliveryAddress
    {
        public int ID { get; set; }
        [Display(Name = "收货人姓名")]
        public string DA_Name { get; set; }
        [Display(Name = "电话")]
        public string DA_Phone { get; set; }
        [Display(Name = "省份")]
        public string DA_Province { get; set; }
        [Display(Name = "城市")]
        public string DA_City { get; set; }
        [Display(Name = "镇")]
        public string DA_Town { get; set; }
        [Display(Name = "详细地址")]
        public string DA_DetailedAddress { get; set; }
        [Display(Name = "邮编")]
        public string DA_ZipCode { get; set; }
        [Display(Name = "是否为默认")]
        public string DA_IsDefault { get; set; }
        [Display(Name = "用户ID")]
        public string U_ID { get; set; }
    }

    public class DeliveryAddressView
    {
        public int ID { get; set; }
        [Display(Name = "收货人姓名")]
        public string DA_Name { get; set; }
        [Display(Name = "电话")]
        public string DA_Phone { get; set; }
        [Display(Name = "省份")]
        public string DA_Province { get; set; }
        [Display(Name = "城市")]
        public string DA_City { get; set; }
        [Display(Name = "镇")]
        public string DA_Town { get; set; }
        [Display(Name = "详细地址")]
        public string DA_DetailedAddress { get; set; }
        [Display(Name = "邮编")]
        public string DA_ZipCode { get; set; }
        [Display(Name = "是否为默认")]
        public string DA_IsDefault { get; set; }
        [Display(Name = "用户ID")]
        public string U_ID { get; set; }
    }
}