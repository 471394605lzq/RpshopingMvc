namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepayorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayOrders", "PayResult", c => c.String());
            AlterColumn("dbo.PayOrders", "PayTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PayOrders", "cash_fee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayOrders", "cash_fee", c => c.Int(nullable: false));
            AlterColumn("dbo.PayOrders", "PayTime", c => c.String());
            DropColumn("dbo.PayOrders", "PayResult");
        }
    }
}
