namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgoodsfavoritesid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_goods", "FavoritesID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_goods", "FavoritesID");
        }
    }
}
