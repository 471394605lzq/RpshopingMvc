namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbrokerageexplain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_goods", "BrokerageExplain", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_goods", "BrokerageExplain");
        }
    }
}
