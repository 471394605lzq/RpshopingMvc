namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sendaddress1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.zyorders", "isactive", c => c.Int(nullable: false));
            AddColumn("dbo.zyorders", "activeid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.zyorders", "activeid");
            DropColumn("dbo.zyorders", "isactive");
        }
    }
}
