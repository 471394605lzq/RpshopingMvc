namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgetpaths : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.goods", "Brand", c => c.Int(nullable: false));
            AddColumn("dbo.goods", "IsRecommend", c => c.Int(nullable: false));
            AddColumn("dbo.goods", "GetPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.goods", "GetPath");
            DropColumn("dbo.goods", "IsRecommend");
            DropColumn("dbo.goods", "Brand");
        }
    }
}
