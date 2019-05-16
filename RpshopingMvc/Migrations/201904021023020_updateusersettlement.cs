namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateusersettlement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSettlements", "OrderCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSettlements", "OrderCode");
        }
    }
}
