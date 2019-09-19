namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusertype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_userinfo", "UserType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_userinfo", "UserType");
        }
    }
}
