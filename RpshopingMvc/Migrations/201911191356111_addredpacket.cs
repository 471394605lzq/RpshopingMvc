namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addredpacket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RedPackets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        userid = c.Int(nullable: false),
                        quota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        packtype = c.Int(nullable: false),
                        CreateTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RedPackets");
        }
    }
}
