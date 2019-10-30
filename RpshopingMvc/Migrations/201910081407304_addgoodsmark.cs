namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgoodsmark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YGoods", "Mark_ID", c => c.Int());
            CreateIndex("dbo.YGoods", "Mark_ID");
            AddForeignKey("dbo.YGoods", "Mark_ID", "dbo.YGoodsTypes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YGoods", "Mark_ID", "dbo.YGoodsTypes");
            DropIndex("dbo.YGoods", new[] { "Mark_ID" });
            DropColumn("dbo.YGoods", "Mark_ID");
        }
    }
}
