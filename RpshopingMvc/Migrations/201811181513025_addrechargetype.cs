namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrechargetype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Recharge", "RechargeType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Recharge", "RechargeType");
        }
    }
}
