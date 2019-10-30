namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addygorder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YGOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        IssueID = c.Int(nullable: false),
                        OrderTime = c.String(),
                        LockCode = c.String(),
                        BuyNumber = c.Int(nullable: false),
                        Paytype = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.YGOrders");
        }
    }
}
