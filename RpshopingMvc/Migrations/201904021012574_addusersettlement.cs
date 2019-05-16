namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusersettlement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSettlements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EstimateIncome = c.String(),
                        SettlementMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SettlementTime = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                        OrderTime = c.DateTime(nullable: false),
                        FromUserAdzoneid = c.String(),
                        SettlementRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BalanceMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserSettlements");
        }
    }
}
