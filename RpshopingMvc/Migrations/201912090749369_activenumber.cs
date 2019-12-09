namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activenumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.zyactivities", "EffectiveTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.zyactivitygoods", "activenumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.zyactivitygoods", "activenumber");
            DropColumn("dbo.zyactivities", "EffectiveTime");
        }
    }
}
