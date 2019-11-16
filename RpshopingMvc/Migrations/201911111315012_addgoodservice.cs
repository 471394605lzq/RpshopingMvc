namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgoodservice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.zygoodservices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Sort = c.Int(nullable: false),
                        Explain = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.zygoodservicetemps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        goodsid = c.Int(nullable: false),
                        serviceid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.zygoodservicetemps");
            DropTable("dbo.zygoodservices");
        }
    }
}
