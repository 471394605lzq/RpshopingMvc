namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_goods", "CategoryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_goods", "CategoryName");
        }
    }
}
