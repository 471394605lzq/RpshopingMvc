namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zyorder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.zyorders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        User_ID = c.Int(nullable: false),
                        OrderCode = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        PayTime = c.String(maxLength: 50),
                        total_fee = c.Int(nullable: false),
                        OrderState = c.Int(nullable: false),
                        GoodsID = c.Int(nullable: false),
                        PayType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.zyorders");
        }
    }
}
