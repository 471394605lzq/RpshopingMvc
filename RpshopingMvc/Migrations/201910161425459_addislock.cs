namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addislock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YGoodsIssues", "IsLock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.YGoodsIssues", "IsLock");
        }
    }
}
