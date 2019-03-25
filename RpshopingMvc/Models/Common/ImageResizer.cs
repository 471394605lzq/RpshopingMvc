﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Models.Common
{
    public class ImageResizer: BaseFileUpload
    {
        public ImageResizer(string name, int width, int height, string image = null,
          int pWidth = 0, int pHeight = 0,
          UploadServer server = UploadServer.QinQiu)
        {
            Name = name;
            Width = width;
            Height = height;
            ImageUrl = image;
            PreviewWidth = pWidth > 0 ? pWidth : width;
            PreviewHeight = pHeight > 0 ? pHeight : height;
            Server = server;
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        private string pPreviewUrl;

        /// <summary>
        /// 预览图片地址
        /// </summary>
        public string PreviewUrl
        {
            get
            {
                if (pPreviewUrl == null)
                {
                    pPreviewUrl = string.IsNullOrWhiteSpace(ImageUrl)
                        ? $"~/Content/Images/phone.jpg?w={PreviewWidth}&h={PreviewHeight}&scale=canvas&Bgcolor=f6f6f6"
                        : Comm.ResizeImage(ImageUrl, PreviewWidth, PreviewHeight);
                }
                return pPreviewUrl;
            }
        }

        /// <summary>
        /// 实际图片宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 实际图片高度
        /// </summary>
        public int Height { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 预览图宽度
        /// </summary>
        public int PreviewWidth { get; set; }

        /// <summary>
        /// 预览图高度
        /// </summary>
        public int PreviewHeight { get; set; }

        /// <summary>
        /// 是否自动初始化
        /// </summary>
        public bool AutoInit { get; set; } = true;
    }
}