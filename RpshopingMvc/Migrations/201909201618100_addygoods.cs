namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addygoods : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YGoods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GoodsName = c.String(),
                        MainImage = c.String(),
                        SamllImage = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        Type_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.YGoodsTypes", t => t.Type_ID)
                .Index(t => t.Type_ID);
            
            CreateTable(
                "dbo.YGoodsTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.YGoodsIssues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IssueNumber = c.Int(nullable: false),
                        State = c.String(),
                        AnnounceTime = c.String(),
                        AlreadyNumber = c.Int(nullable: false),
                        SurplusNumber = c.Int(nullable: false),
                        SumNumber = c.Int(nullable: false),
                        LuckCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YGoods", "Type_ID", "dbo.YGoodsTypes");
            DropIndex("dbo.YGoods", new[] { "Type_ID" });
            DropTable("dbo.YGoodsIssues");
            DropTable("dbo.YGoodsTypes");
            DropTable("dbo.YGoods");
        }
    }
}
