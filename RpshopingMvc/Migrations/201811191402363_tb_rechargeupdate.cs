namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tb_rechargeupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Recharge", "paytype", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Recharge", "PA_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Recharge", "PA_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_Recharge", "paytype", c => c.String());
        }
    }
}
