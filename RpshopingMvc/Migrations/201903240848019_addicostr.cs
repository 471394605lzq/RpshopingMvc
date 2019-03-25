namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addicostr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_goodssort", "ICO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_goodssort", "ICO");
        }
    }
}
