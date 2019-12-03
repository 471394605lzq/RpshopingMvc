using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RpshopingMvc.Models.Common;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    //轮播图
    public class Selide
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "图片链接")]
        public string ImagePath { get; set; }
        [Display(Name = "排序编号")]
        public int Index { get; set; }
        [Display(Name = "类别")]
        public SelideType SelideType { get; set; }
        [Display(Name = "产品编号")]
        public int GoodsID { get; set; }
        [Display(Name = "产品名称")]
        public string  GoodsName { get; set; }
    }
    public class SelideShow
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "图片链接")]
        public string ImagePath { get; set; }
        [Display(Name = "排序编号")]
        public int Index { get; set; }
        [Display(Name = "类别")]
        public SelideType SelideType { get; set; }
        [Display(Name = "产品编号")]
        public int GoodsID { get; set; }
        [Display(Name = "产品名称")]
        public string GoodsName { get; set; }
    }
    public class SelideView
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "图片链接")]
        public FileUpload ImagePath { get; set; } = new FileUpload
        {
            AutoInit = true,
            Max = 5,
            Name = "ImagePath",
            Server = Models.Common.UploadServer.QinQiu,
            Sortable = true,
            Type = Models.Common.FileType.Image,
        };

        [Display(Name = "排序编号")]
        public int Index { get; set; }
        [Display(Name = "类别")]
        public SelideType SelideType { get; set; }
        [Display(Name = "产品编号")]
        public int GoodsID { get; set; }
        [Display(Name = "产品名称")]
        public string GoodsName { get; set; }
    }
}