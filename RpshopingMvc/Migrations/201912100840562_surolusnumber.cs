namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class surolusnumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.zyactivitygoods", "surplusnumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.zyactivitygoods", "surplusnumber");
        }
    }
}
