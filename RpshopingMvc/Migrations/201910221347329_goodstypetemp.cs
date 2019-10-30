namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goodstypetemp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.goodstypetemps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        goodstypeid = c.Int(nullable: false),
                        goodsid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.goodstypetemps");
        }
    }
}
