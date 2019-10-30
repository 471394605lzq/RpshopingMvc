namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ssd1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.YGoods", "Mark_ID", "dbo.YGoodsTypes");
            DropIndex("dbo.YGoods", new[] { "Mark_ID" });
            AddColumn("dbo.YGoods", "Mark", c => c.Int(nullable: false));
            DropColumn("dbo.YGoods", "Mark_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.YGoods", "Mark_ID", c => c.Int());
            DropColumn("dbo.YGoods", "Mark");
            CreateIndex("dbo.YGoods", "Mark_ID");
            AddForeignKey("dbo.YGoods", "Mark_ID", "dbo.YGoodsTypes", "ID");
        }
    }
}
