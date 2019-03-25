using RpshopingMvc.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class tb_goodssort
    {
        public int ID { get; set; }
        [Display(Name ="分类名称")]
        [MaxLength(50)]
        public string SortName { get; set; }
        [Display(Name ="排序")]
        public int SortIndex { get; set; }
        [Display(Name ="分类等级")]
        public int Grade { get; set; }
        [Display(Name ="上级分类ID")]
        public int ParentID { get; set; }
        [Display(Name ="分类图片地址")]
        [MaxLength(200)]
        public string ImagePath { get; set; }
        [Display(Name = "选品库ID")]
        public string FID { get; set; }
    }


    public class tb_goodssortview
    {
        public int ID { get; set; }
        [Display(Name = "分类名称")]
        [MaxLength(50)]
        public string SortName { get; set; }
        [Display(Name = "排序")]
        public int SortIndex { get; set; }
        [Display(Name = "分类等级")]
        public int Grade { get; set; }
        [Display(Name = "上级分类")]
        public int ParentID { get; set; }
        [Display(Name = "分类图片")]
        //[MaxLength(200)]
        //public string ImagePath { get; set; }
        //public RpshopingMvc.Models.Common.ImageResizer ImagePath { get; set; } = new Common.ImageResizer("ImagePath", 120, 120)
        //{
        //    AutoInit = true,
        //    Name = "ImagePath",
        //    Server = UploadServer.QinQiu,
        //};

        public FileUpload ImagePath { get; set; } = new FileUpload
        {
            AutoInit = true,
            Max = 5,
            Name = "ImagePath",
            Server = Models.Common.UploadServer.QinQiu,
            Sortable = true,
            Type = Models.Common.FileType.Image,
        };
    }
    public class tb_goodssortshow
    {
        public int ID { get; set; }
        [Display(Name = "分类名称")]
        public string SortName { get; set; }
        [Display(Name = "排序")]
        public int SortIndex { get; set; }
        [Display(Name = "分类等级")]
        public int Grade { get; set; }
        [Display(Name = "上级分类ID")]
        public int ParentID { get; set; }
        [Display(Name = "分类图片地址")]
        public string ImagePath { get; set; }
    }
}