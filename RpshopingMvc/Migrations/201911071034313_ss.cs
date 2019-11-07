namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_userinfo", "WXOpenid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_userinfo", "WXOpenid");
        }
    }
}
