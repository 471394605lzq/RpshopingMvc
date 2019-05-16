namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtborders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tborders", "StoreName", c => c.String());
            AddColumn("dbo.Tborders", "IncomeRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tborders", "OrderType", c => c.String());
            AddColumn("dbo.Tborders", "GoodsPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tborders", "EstimateIncome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tborders", "SettlementMoney", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tborders", "EffectIncome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tborders", "SettlementTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tborders", "AdsenseID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tborders", "AdsenseID");
            DropColumn("dbo.Tborders", "SettlementTime");
            DropColumn("dbo.Tborders", "EffectIncome");
            DropColumn("dbo.Tborders", "SettlementMoney");
            DropColumn("dbo.Tborders", "EstimateIncome");
            DropColumn("dbo.Tborders", "GoodsPrice");
            DropColumn("dbo.Tborders", "OrderType");
            DropColumn("dbo.Tborders", "IncomeRate");
            DropColumn("dbo.Tborders", "StoreName");
        }
    }
}
