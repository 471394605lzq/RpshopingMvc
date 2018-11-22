namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tb_userinfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_userinfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Integral = c.Int(nullable: false),
                        FirstCharge = c.Int(nullable: false),
                        RewardMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_userinfo");
        }
    }
}
