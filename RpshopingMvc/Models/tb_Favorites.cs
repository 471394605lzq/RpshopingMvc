using RpshopingMvc.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models
{
    public class tb_Favorites
    {
        public int ID { get; set; }

        [Display(Name = "选品库名称")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "选品库图片地址")]
        [MaxLength(50)]
        public string ImagePath { get; set; }
        [Display(Name = "选品库简介说明")]
        [MaxLength(50)]
        public string Explain { get; set; }
        [Display(Name = "选品库ID")]
        [MaxLength(50)]
        public string FavoritesID { get; set; }
        [Display(Name = "类型")]
        public FavoritesType Type { get; set; }
        [Display(Name = "对应图标类名")]
        public string ICO { get; set; }
    }

    public class tb_Favoritesview
    {
        public int ID { get; set; }

        [Display(Name = "选品库名称")]
        public string Name { get; set; }

        [Display(Name = "选品库图片地址")]
        public FileUpload ImagePath { get; set; } = new FileUpload
        {
            AutoInit = true,
            Max = 5,
            Name = "ImagePath",
            Server = Models.Common.UploadServer.QinQiu,
            Sortable = true,
            Type = Models.Common.FileType.Image,
        };
        [Display(Name = "选品库简介说明")]
        public string Explain { get; set; }
        [Display(Name = "选品库ID")]
        public string FavoritesID { get; set; }
        [Display(Name = "类型")]
        public FavoritesType Type { get; set; }
        [Display(Name = "对应图标类名")]
        public string ICO { get; set; }
    }

    public class tb_Favoritesshow
    {
        public int ID { get; set; }

        [Display(Name = "选品库名称")]
        public string Name { get; set; }

        [Display(Name = "选品库图片地址")]
        public string ImagePath { get; set; }
        [Display(Name = "选品库简介说明")]
        public string Explain { get; set; }
        [Display(Name = "选品库ID")]
        public string FavoritesID { get; set; }
        [Display(Name = "类型")]
        public FavoritesType Type { get; set; }
        [Display(Name = "对应图标类名")]
        public string ICO { get; set; }
    }
}