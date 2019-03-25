namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addzkprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_goods", "wxzkprice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tb_goods", "zkprice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_goods", "zkprice");
            DropColumn("dbo.tb_goods", "wxzkprice");
        }
    }
}
