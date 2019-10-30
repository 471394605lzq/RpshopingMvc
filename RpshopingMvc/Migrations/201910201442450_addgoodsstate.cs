namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgoodsstate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.goods", "ByIndex", c => c.Int(nullable: false));
            AddColumn("dbo.goods", "GoodsState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.goods", "GoodsState");
            DropColumn("dbo.goods", "ByIndex");
        }
    }
}
