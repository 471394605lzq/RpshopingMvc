using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class RedPacket
    {
        public int ID { get; set; }
        [Display(Name = "用户id")]
        public int userid { get; set; }
        [Display(Name = "红包额度")]
        public decimal quota { get; set; }
        [Display(Name = "红包类型")]
        public RedPacketType packtype { get; set; }
        [Display(Name = "红包领取时间")]
        public string CreateTime { get; set; }
        [Display(Name = "红包标题")]
        public string Title { get; set; }
    }

    public class RedPacketshow
    {
        public int userid { get; set; }
        [Display(Name = "红包额度")]
        public decimal quota { get; set; }
        [Display(Name = "红包类型")]
        public RedPacketType packtype { get; set; }
        [Display(Name = "红包领取时间")]
        public string CreateTime { get; set; }
        [Display(Name = "红包标题")]
        public string Title { get; set; }
    }
}