namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class selidegoodsid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Selides", "GoodsID", c => c.Int(nullable: false));
            AddColumn("dbo.Selides", "GoodsName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Selides", "GoodsName");
            DropColumn("dbo.Selides", "GoodsID");
        }
    }
}
