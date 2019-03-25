namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorderstate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tborders", "OrderState", c => c.Int(nullable: false));
            DropColumn("dbo.Tborders", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tborders", "MyProperty", c => c.Int(nullable: false));
            DropColumn("dbo.Tborders", "OrderState");
        }
    }
}
