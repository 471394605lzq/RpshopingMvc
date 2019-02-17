namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PayOrders", "User_ID", c => c.String(maxLength: 50));
            AlterColumn("dbo.PayOrders", "PayTime", c => c.String(maxLength: 50));
            AlterColumn("dbo.PayOrders", "PayPrepay_id", c => c.String(maxLength: 50));
            AlterColumn("dbo.PayOrders", "out_trade_no", c => c.String(maxLength: 50));
            AlterColumn("dbo.PayOrders", "Paynoncestr", c => c.String(maxLength: 50));
            AlterColumn("dbo.PayOrders", "device_info", c => c.String(maxLength: 50));
            AlterColumn("dbo.PayOrders", "spbill_create_ip", c => c.String(maxLength: 50));
            AlterColumn("dbo.PayOrders", "transaction_id", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_Favorites", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_Favorites", "ImagePath", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_Favorites", "Explain", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_goods", "Code", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_goods", "SellerID", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_goods", "StoreName", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_goods", "StoreImage", c => c.String(maxLength: 200));
            AlterColumn("dbo.tb_goods", "PlatformType", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_goods", "CouponID", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_goods", "CouponCount", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_goods", "CouponSurplus", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_goods", "CouponDenomination", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tb_goods", "CouponStartTime", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_goods", "CouponEndTime", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_goodssort", "SortName", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_goodssort", "ImagePath", c => c.String(maxLength: 200));
            AlterColumn("dbo.tb_Recharge", "UserID", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_TKInfo", "Memberid", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_TKInfo", "Siteid", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_TKInfo", "Adzoneid", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_userinfo", "UserID", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_userinfo", "UserID", c => c.String());
            AlterColumn("dbo.tb_TKInfo", "Adzoneid", c => c.String());
            AlterColumn("dbo.tb_TKInfo", "Siteid", c => c.String());
            AlterColumn("dbo.tb_TKInfo", "Memberid", c => c.String());
            AlterColumn("dbo.tb_Recharge", "UserID", c => c.String());
            AlterColumn("dbo.tb_goodssort", "ImagePath", c => c.String());
            AlterColumn("dbo.tb_goodssort", "SortName", c => c.String());
            AlterColumn("dbo.tb_goods", "CouponEndTime", c => c.String());
            AlterColumn("dbo.tb_goods", "CouponStartTime", c => c.String());
            AlterColumn("dbo.tb_goods", "CouponDenomination", c => c.String());
            AlterColumn("dbo.tb_goods", "CouponSurplus", c => c.String());
            AlterColumn("dbo.tb_goods", "CouponCount", c => c.String());
            AlterColumn("dbo.tb_goods", "CouponID", c => c.String());
            AlterColumn("dbo.tb_goods", "PlatformType", c => c.String());
            AlterColumn("dbo.tb_goods", "StoreImage", c => c.String());
            AlterColumn("dbo.tb_goods", "StoreName", c => c.String());
            AlterColumn("dbo.tb_goods", "SellerID", c => c.String());
            AlterColumn("dbo.tb_goods", "Code", c => c.String());
            AlterColumn("dbo.tb_Favorites", "Explain", c => c.String());
            AlterColumn("dbo.tb_Favorites", "ImagePath", c => c.String());
            AlterColumn("dbo.tb_Favorites", "Name", c => c.String());
            AlterColumn("dbo.PayOrders", "transaction_id", c => c.String());
            AlterColumn("dbo.PayOrders", "spbill_create_ip", c => c.String());
            AlterColumn("dbo.PayOrders", "device_info", c => c.String());
            AlterColumn("dbo.PayOrders", "Paynoncestr", c => c.String());
            AlterColumn("dbo.PayOrders", "out_trade_no", c => c.String());
            AlterColumn("dbo.PayOrders", "PayPrepay_id", c => c.String());
            AlterColumn("dbo.PayOrders", "PayTime", c => c.String());
            AlterColumn("dbo.PayOrders", "User_ID", c => c.String());
        }
    }
}
