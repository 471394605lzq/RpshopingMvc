using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpshopingMvc.Models.Common
{
    public class SysRole
    {
        #region 系统权限


        public const string RoleManageRead = "RoleManageRead";
        public const string RoleManageCreate = "RoleManageCreate";
        public const string RoleManageEdit = "RoleManageEdit";
        public const string RoleManageDelete = "RoleManageDelete";


        public const string UserManageRead = "SystemUserManageRead";
        public const string UserManageCreate = "SystemUserManageCreate";
        public const string UserManageEdit = "SystemUserManageEdit";
        public const string UserManageDelete = "SystemUserManageDelete";

        public const string ProductManageRead = "ProductManageRead";
        public const string ProductManageCreate = "ProductManageCreate";
        public const string ProductManageEdit = "ProductManageEdit";
        public const string ProductMangeDelete = "ProductMangeDelete";

        public const string ProductKindManageRead = "ProductKindManageRead";
        public const string ProductKindManageCreate = "ProductKindManageCreate";
        public const string ProductKindManageEdit = "ProductKindManageEdit";
        public const string ProductKindManageDelete = "ProductKindManageDelete";

        #endregion
    }
}