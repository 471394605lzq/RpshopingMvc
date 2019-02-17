namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updategoods : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_goods", "GoodsName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_goods", "GoodsName", c => c.String());
        }
    }
}
