namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserinvitationaward : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInvitationAwards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 50),
                        AwardMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateTime = c.String(),
                        FromUserID = c.String(),
                        States = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInvitationAwards");
        }
    }
}
