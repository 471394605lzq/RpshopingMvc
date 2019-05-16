namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addwithdrawcash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_userinfo", "AliAccount", c => c.String(maxLength: 50));
            AddColumn("dbo.tb_userinfo", "AliUserName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_userinfo", "AliUserName");
            DropColumn("dbo.tb_userinfo", "AliAccount");
        }
    }
}
