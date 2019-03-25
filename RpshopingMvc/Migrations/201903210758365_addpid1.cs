namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpid1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_userinfo", "Memberid", c => c.String(maxLength: 50));
            AddColumn("dbo.tb_userinfo", "Siteid", c => c.String(maxLength: 50));
            AddColumn("dbo.tb_userinfo", "Adzoneid", c => c.String(maxLength: 50));
            AddColumn("dbo.tb_userinfo", "PID", c => c.String(maxLength: 100));
            AddColumn("dbo.tb_userinfo", "UserCode", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_userinfo", "UserCode");
            DropColumn("dbo.tb_userinfo", "PID");
            DropColumn("dbo.tb_userinfo", "Adzoneid");
            DropColumn("dbo.tb_userinfo", "Siteid");
            DropColumn("dbo.tb_userinfo", "Memberid");
        }
    }
}
