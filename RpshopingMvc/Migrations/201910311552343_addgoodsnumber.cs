namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgoodsnumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.zyorders", "GoodsNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.zyorders", "GoodsNumber");
        }
    }
}
