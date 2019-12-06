namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.zyorders", "RedID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.zyorders", "RedID");
        }
    }
}
