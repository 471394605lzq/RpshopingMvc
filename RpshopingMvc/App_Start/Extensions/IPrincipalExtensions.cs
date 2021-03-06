﻿using RpshopingMvc.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpshopingMvc
{
    public static class IPrincipalExtensions
    {
        public static List<Menu> Menus(this System.Security.Principal.IPrincipal p)
        {
            var menus = new List<Menu>();
            menus.Add(new Menu { Name = "用户管理", Title = "用户管理", Url = "~/Userinfo/Index", IconImage = "yonghu" });
            //menus.Add(new Menu { Name = "权限管理", Title = "权限管理", Url = "~/RoleManage/Index", IconImage = "moban" });
            menus.Add(new Menu { Name = "商品分类等级", Title = "商品分类等级", Url = "~/GoodsSortGrades/Index", IconImage = "fenlei-" });
            menus.Add(new Menu { Name = "商品分类管理", Title = "商品分类管理", Url = "~/tb_goodssort/Index", IconImage = "fenlei-" });
            menus.Add(new Menu { Name = "商品管理", Title = "商品管理", Url = "~/tb_goods/Index", IconImage = "shangpin" });
            menus.Add(new Menu { Name = "选品库管理", Title = "选品库管理", Url = "~/tb_Favorites/Index", IconImage = "chanpinku" });
            menus.Add(new Menu { Name = "学院管理", Title = "学院管理", Url = "~/CollegeInfoes/Index", IconImage = "xueyuan" });
            menus.Add(new Menu { Name = "云购商品分类", Title = "云购商品分类", Url = "~/YGoodsTypes/Index", IconImage = "fenlei_" });
            menus.Add(new Menu { Name = "云购商品", Title = "云购商品", Url = "~/YGoods/Index", IconImage = "shangpin" });
            menus.Add(new Menu { Name = "云购期数", Title = "云购期数", Url = "~/YGoodsIssues/Index", IconImage = "zhuantishenhedan-qishu" });

            menus.Add(new Menu { Name = "自营商品分类", Title = "自营商品分类", Url = "~/goodstypes/Index", IconImage = "shuangsechangyongtubiao-" });
            menus.Add(new Menu { Name = "自营商品", Title = "自营商品", Url = "~/goods/Index", IconImage = "shangpin1" });
            menus.Add(new Menu { Name = "品牌", Title = "品牌", Url = "~/Brands/Index", IconImage = "pinpaiguanli" });
            menus.Add(new Menu { Name = "自营商品服务", Title = "自营商品服务", Url = "~/zygoodservices/Index", IconImage = "fuwu" });
            menus.Add(new Menu { Name = "商品活动", Title = "商品活动", Url = "~/zyactivities/Index", IconImage = "huodong" });
            menus.Add(new Menu { Name = "轮播图", Title = "轮播图", Url = "~/Selides/Index", IconImage = "huodong" });
            


            //if (p.IsInRole(SysRole.UserManageRead))
            //{
            //    menus.Add(new Menu { Name = "用户管理", Title = "用户管理", Url = "~/UserManage/Index", IconImage = "yonghu" });
            //}
            //if (p.IsInRole(SysRole.RoleManageRead))
            //{
            //    menus.Add(new Menu { Name = "权限管理", Title = "权限管理", Url = "~/RoleManage/Index", IconImage = "moban" })
            //}
            //if (p.IsInRole(SysRole.ProductKindManageRead))
            //{
            //    menus.Add(new Menu { Name = "商品分类管理", Title = "商品分类管理", Url = "~/ProductKindsManage/Index", IconImage = "fenlei-" });
            //}
            //if (p.IsInRole(SysRole.ProductManageRead))
            //{
            //    menus.Add(new Menu { Name = "商品管理", Title = "商品管理", Url = "~/ProductsManage/Index", IconImage = "shangpin" });
            //}

            return menus;
        }
    }
}