using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class zyactivity
    {
        public int ID { get; set; }
        [Display(Name = "活动名称")]
        public string Name { get; set; }
        [Display(Name = "用户等级要求")]
        public UserGrade GradeAsk { get; set; }
    }
    public class zyactivityshow
    {
        public int ID { get; set; }
        [Display(Name = "活动名称")]
        public string Name { get; set; }
        [Display(Name = "用户等级要求")]
        public string GradeAsk { get; set; }
    }

    public class zyactivityview
    {
        public int ID { get; set; }
        [Display(Name = "活动名称")]
        public string Name { get; set; }
        [Display(Name = "用户等级要求")]
        public UserGrade GradeAsk { get; set; }
    }
}