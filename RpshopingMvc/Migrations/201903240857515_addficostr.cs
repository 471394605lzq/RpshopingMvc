namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addficostr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Favorites", "ICO", c => c.String());
            DropColumn("dbo.tb_goodssort", "ICO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_goodssort", "ICO", c => c.String());
            DropColumn("dbo.tb_Favorites", "ICO");
        }
    }
}
