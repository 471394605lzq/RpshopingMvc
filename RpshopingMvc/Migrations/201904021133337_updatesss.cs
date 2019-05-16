namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserSettlements", "EstimateIncome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserSettlements", "EstimateIncome", c => c.String());
        }
    }
}
