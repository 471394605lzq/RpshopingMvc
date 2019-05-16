namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuserinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_userinfo", "ThisMonthEstimateIncome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tb_userinfo", "ThisMonthSettlementMoney", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tb_userinfo", "LastMonthEstimateIncome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tb_userinfo", "LastMonthSettlementMoney", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_userinfo", "LastMonthSettlementMoney");
            DropColumn("dbo.tb_userinfo", "LastMonthEstimateIncome");
            DropColumn("dbo.tb_userinfo", "ThisMonthSettlementMoney");
            DropColumn("dbo.tb_userinfo", "ThisMonthEstimateIncome");
        }
    }
}
