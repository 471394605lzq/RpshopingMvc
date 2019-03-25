namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addparnetid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_userinfo", "UserGrade", c => c.Int(nullable: false));
            AddColumn("dbo.tb_userinfo", "ParentID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_userinfo", "ParentID");
            DropColumn("dbo.tb_userinfo", "UserGrade");
        }
    }
}
