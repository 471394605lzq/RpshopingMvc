namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatezyorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.zyorders", "DeliveryAddressID", c => c.Int(nullable: false));
            AddColumn("dbo.zyorders", "BuyerLeave", c => c.String());
            AddColumn("dbo.zyorders", "ExpressCode", c => c.String());
            AddColumn("dbo.zyorders", "Postage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.zyorders", "Postage");
            DropColumn("dbo.zyorders", "ExpressCode");
            DropColumn("dbo.zyorders", "BuyerLeave");
            DropColumn("dbo.zyorders", "DeliveryAddressID");
        }
    }
}
