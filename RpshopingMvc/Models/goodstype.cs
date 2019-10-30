using RpshopingMvc.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class goodstype
    {
        public int ID { get; set; }
        [Display(Name = "分类名称")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Display(Name = "排序")]
        public int SortIndex { get; set; }
        [Display(Name = "上级分类ID")]
        public int ParentID { get; set; }
        [Display(Name = "分类图片地址")]
        [MaxLength(200)]
        public string ImagePath { get; set; }
    }

    public class goodstypeview
    {
        public int ID { get; set; }
        [Display(Name = "分类名称")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Display(Name = "排序")]
        public int SortIndex { get; set; }
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
    public class goodstypeshow
    {
        public int ID { get; set; }
        [Display(Name = "分类名称")]
        public string Name { get; set; }
        [Display(Name = "排序")]
        public int SortIndex { get; set; }
        [Display(Name = "上级分类ID")]
        public int ParentID { get; set; }
        [Display(Name = "分类图片地址")]
        public string ImagePath { get; set; }
    }
}