namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goodstyperq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.YGoodsTypes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.YGoodsTypes", "Name", c => c.String());
        }
    }
}
