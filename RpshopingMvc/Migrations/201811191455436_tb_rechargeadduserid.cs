namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tb_rechargeadduserid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Recharge", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Recharge", "UserID");
        }
    }
}
