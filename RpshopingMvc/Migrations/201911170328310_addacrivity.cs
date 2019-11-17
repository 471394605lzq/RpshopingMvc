namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addacrivity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.zyactivities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GradeAsk = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.zyactivitygoods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        goodsid = c.Int(nullable: false),
                        activityid = c.Int(nullable: false),
                        Postage = c.Int(nullable: false),
                        acrivityprice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.zyactivitygoods");
            DropTable("dbo.zyactivities");
        }
    }
}
