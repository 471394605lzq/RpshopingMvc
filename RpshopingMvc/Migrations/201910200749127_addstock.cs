namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.goods", "Stock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.goods", "Stock");
        }
    }
}
