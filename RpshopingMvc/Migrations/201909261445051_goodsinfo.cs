namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goodsinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YGoods", "Info", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.YGoods", "Info");
        }
    }
}
