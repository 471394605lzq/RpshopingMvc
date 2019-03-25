namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesortfids : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_goodssort", "FID", c => c.String());
            DropColumn("dbo.tb_goodssort", "FavoritesID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_goodssort", "FavoritesID", c => c.Long(nullable: false));
            DropColumn("dbo.tb_goodssort", "FID");
        }
    }
}
