namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTborder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tborders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        YUserID = c.String(),
                        UserID = c.Int(nullable: false),
                        OrderTime = c.DateTime(nullable: false),
                        OrderCode = c.String(),
                        GoodsID = c.Int(nullable: false),
                        GoodsName = c.String(),
                        OrderPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BalanceTime = c.DateTime(nullable: false),
                        RebateMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GoodsImage = c.String(),
                        MyProperty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tborders");
        }
    }
}
