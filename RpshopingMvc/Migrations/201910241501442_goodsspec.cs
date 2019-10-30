namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goodsspec : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.goods", "Specs", c => c.String());
            DropColumn("dbo.goods", "GoodsType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.goods", "GoodsType", c => c.Int(nullable: false));
            DropColumn("dbo.goods", "Specs");
        }
    }
}
