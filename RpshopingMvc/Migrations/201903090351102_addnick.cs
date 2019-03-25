namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnick : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_goods", "StoreNick", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_goods", "StoreNick");
        }
    }
}
