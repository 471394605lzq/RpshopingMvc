namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sendaddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.goods", "SendAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.goods", "SendAddress");
        }
    }
}
