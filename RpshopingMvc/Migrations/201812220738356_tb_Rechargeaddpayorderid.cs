namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tb_Rechargeaddpayorderid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Recharge", "PayOrderID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Recharge", "PayOrderID");
        }
    }
}
