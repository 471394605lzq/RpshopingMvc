namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsettlementstate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSettlements", "SettlementState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSettlements", "SettlementState");
        }
    }
}
