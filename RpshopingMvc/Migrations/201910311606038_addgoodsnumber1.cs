namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgoodsnumber1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.zyorders", "total_fee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.zyorders", "PayType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.zyorders", "PayType", c => c.Int(nullable: false));
            AlterColumn("dbo.zyorders", "total_fee", c => c.Int(nullable: false));
        }
    }
}
