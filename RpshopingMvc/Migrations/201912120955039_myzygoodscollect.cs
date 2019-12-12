namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class myzygoodscollect : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyZyGoodsCollects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        goodid = c.Int(nullable: false),
                        userid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MyZyGoodsCollects");
        }
    }
}
