namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addredtitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RedPackets", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RedPackets", "Title");
        }
    }
}
