namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sort : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YGoodsTypes", "Sort", c => c.Int(nullable: false));
            AddColumn("dbo.YGoodsTypes", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.YGoodsTypes", "Icon");
            DropColumn("dbo.YGoodsTypes", "Sort");
        }
    }
}
