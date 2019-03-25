namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsortfid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_goodssort", "FavoritesID", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_goodssort", "FavoritesID");
        }
    }
}
