using RpshopingMvc.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models
{
    public class Brand
    {
        public int ID { get; set; }
        [Display(Name = "品牌名称")]
        public string Name { get; set; }
        [Display(Name = "图片")]
        public string Image { get; set; }
        [Display(Name = "排序")]
        public int Sort { get; set; }
        [Display(Name = "说明")]
        public string Explain { get; set; }

    }
    public class BrandShow
    {
        public int ID { get; set; }
        [Display(Name = "品牌名称")]
        public string Name { get; set; }
        [Display(Name = "图片")]
        public string Image { get; set; }
        [Display(Name = "排序")]
        public int Sort { get; set; }
        [Display(Name = "说明")]
        public string Explain { get; set; }

    }
    public class BrandView
    {
        public int ID { get; set; }
        [Display(Name = "品牌名称")]
        public string Name { get; set; }
        [Display(Name = "图片")]
        public FileUpload Image { get; set; } = new FileUpload
        {
            AutoInit = true,
            Max = 5,
            Name = "Image",
            Server = Models.Common.UploadServer.QinQiu,
            Sortable = true,
            Type = Models.Common.FileType.Image,
        };
        [Display(Name = "排序")]
        public int Sort { get; set; }
        [Display(Name = "说明")]
        public string Explain { get; set; }
    }

}