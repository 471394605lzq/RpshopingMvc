namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class brandexplain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "Explain", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Brands", "Explain");
        }
    }
}
