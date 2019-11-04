namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbalancedetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BalanceDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        CountBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MoverBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MoverType = c.Int(nullable: false),
                        MoverTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BalanceDetails");
        }
    }
}
