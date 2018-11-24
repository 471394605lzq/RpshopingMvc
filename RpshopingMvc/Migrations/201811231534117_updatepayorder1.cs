namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepayorder1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PayOrders", "PayTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayOrders", "PayTime", c => c.DateTime(nullable: false));
        }
    }
}
