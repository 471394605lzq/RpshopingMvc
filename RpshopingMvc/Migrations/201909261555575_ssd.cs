namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ssd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.YGoods", "Type_ID", "dbo.YGoodsTypes");
            DropIndex("dbo.YGoods", new[] { "Type_ID" });
            AddColumn("dbo.YGoods", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.YGoods", "Type_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.YGoods", "Type_ID", c => c.Int());
            DropColumn("dbo.YGoods", "Type");
            CreateIndex("dbo.YGoods", "Type_ID");
            AddForeignKey("dbo.YGoods", "Type_ID", "dbo.YGoodsTypes", "ID");
        }
    }
}
