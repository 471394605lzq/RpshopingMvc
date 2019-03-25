namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgoodstype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_goods", "GoodsType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_goods", "GoodsType");
        }
    }
}
