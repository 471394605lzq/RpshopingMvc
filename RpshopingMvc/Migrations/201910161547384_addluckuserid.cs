namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addluckuserid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YGoodsIssues", "LuckUserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.YGoodsIssues", "LuckUserID");
        }
    }
}
