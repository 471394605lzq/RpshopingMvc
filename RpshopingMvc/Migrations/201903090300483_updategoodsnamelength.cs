namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updategoodsnamelength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_goods", "GoodsName", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_goods", "GoodsName", c => c.String(maxLength: 50));
        }
    }
}
