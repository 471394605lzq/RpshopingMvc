namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatezyorder1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PayOrders", "settlement_total_fee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PayOrders", "total_fee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayOrders", "total_fee", c => c.Int(nullable: false));
            AlterColumn("dbo.PayOrders", "settlement_total_fee", c => c.Int(nullable: false));
        }
    }
}
