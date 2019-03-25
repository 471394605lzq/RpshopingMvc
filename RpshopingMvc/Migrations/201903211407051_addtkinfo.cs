namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtkinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_TKInfo", "PID", c => c.String());
            AddColumn("dbo.tb_TKInfo", "PIDState", c => c.Int(nullable: false));
            AddColumn("dbo.tb_TKInfo", "UID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_TKInfo", "UID");
            DropColumn("dbo.tb_TKInfo", "PIDState");
            DropColumn("dbo.tb_TKInfo", "PID");
        }
    }
}
