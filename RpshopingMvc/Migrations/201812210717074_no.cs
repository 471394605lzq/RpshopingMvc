namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class no : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayOrders", "OrderType", c => c.Int(nullable: false));
            AddColumn("dbo.PayOrders", "RelationID", c => c.Int(nullable: false));
            AlterColumn("dbo.PayOrders", "User_ID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayOrders", "User_ID", c => c.Int(nullable: false));
            DropColumn("dbo.PayOrders", "RelationID");
            DropColumn("dbo.PayOrders", "OrderType");
        }
    }
}
