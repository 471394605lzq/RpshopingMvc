namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addorder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PayOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        User_ID = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        PayTime = c.String(),
                        PayPrepay_id = c.String(),
                        out_trade_no = c.String(),
                        Paynoncestr = c.String(),
                        settlement_total_fee = c.Int(nullable: false),
                        total_fee = c.Int(nullable: false),
                        cash_fee = c.Int(nullable: false),
                        device_info = c.String(),
                        spbill_create_ip = c.String(),
                        Sign = c.String(),
                        OrderState = c.Int(nullable: false),
                        transaction_id = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PayOrders");
        }
    }
}
