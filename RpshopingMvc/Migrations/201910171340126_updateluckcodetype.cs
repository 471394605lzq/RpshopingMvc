namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateluckcodetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.YGoodsIssues", "LuckCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.YGoodsIssues", "LuckCode", c => c.Int(nullable: false));
        }
    }
}
