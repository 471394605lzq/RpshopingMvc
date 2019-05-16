namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorders : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tborders", "GoodsID", c => c.String());
            DropColumn("dbo.Tborders", "YUserID");
            DropColumn("dbo.Tborders", "UserID");
            DropColumn("dbo.Tborders", "BalanceTime");
            DropColumn("dbo.Tborders", "RebateMoney");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tborders", "RebateMoney", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tborders", "BalanceTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tborders", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Tborders", "YUserID", c => c.String());
            AlterColumn("dbo.Tborders", "GoodsID", c => c.Int(nullable: false));
        }
    }
}
