namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtbuserid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_userinfo", "tbuserid", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_userinfo", "tbuserid");
        }
    }
}
