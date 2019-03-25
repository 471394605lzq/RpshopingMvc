namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefavoretes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Favorites", "FavoritesID", c => c.String(maxLength: 50));
            AddColumn("dbo.tb_Favorites", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Favorites", "Type");
            DropColumn("dbo.tb_Favorites", "FavoritesID");
        }
    }
}
