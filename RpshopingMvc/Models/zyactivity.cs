using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class zyactivity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public UserGrade GradeAsk { get; set; }

    }
}