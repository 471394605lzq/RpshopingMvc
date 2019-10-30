namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addyggoodsid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YGoodsIssues", "YGoodsId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.YGoodsIssues", "YGoodsId");
        }
    }
}
