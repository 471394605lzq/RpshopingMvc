namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgoods : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Favorites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImagePath = c.String(),
                        Explain = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tb_goods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GoodsName = c.String(),
                        Code = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImagePath = c.String(),
                        SmallImages = c.String(),
                        DetailPath = c.String(),
                        GoodsSort = c.Int(nullable: false),
                        TkPath = c.String(),
                        SalesVolume = c.Int(nullable: false),
                        IncomeRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Brokerage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellerID = c.String(),
                        StoreName = c.String(),
                        StoreImage = c.String(),
                        PlatformType = c.String(),
                        CouponID = c.String(),
                        CouponCount = c.String(),
                        CouponSurplus = c.String(),
                        CouponDenomination = c.String(),
                        CouponStartTime = c.String(),
                        CouponEndTime = c.String(),
                        CouponPath = c.String(),
                        CouponSpreadPath = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tb_goodssort",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SortName = c.String(),
                        SortIndex = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                        ParentID = c.Int(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tb_TKInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Memberid = c.String(),
                        Siteid = c.String(),
                        Adzoneid = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_TKInfo");
            DropTable("dbo.tb_goodssort");
            DropTable("dbo.tb_goods");
            DropTable("dbo.tb_Favorites");
        }
    }
}
