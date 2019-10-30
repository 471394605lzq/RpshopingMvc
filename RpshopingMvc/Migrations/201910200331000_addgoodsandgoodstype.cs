namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgoodsandgoodstype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.goods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GoodsName = c.String(maxLength: 200),
                        Code = c.String(maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        zkprice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImagePath = c.String(),
                        SmallImages = c.String(),
                        DetailPath = c.String(),
                        GoodsType = c.Int(nullable: false),
                        SalesVolume = c.Int(nullable: false),
                        IncomeRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Brokerage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BrokerageExplain = c.String(),
                        Postage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.goodstypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SortName = c.String(maxLength: 50),
                        SortIndex = c.Int(nullable: false),
                        ParentID = c.Int(nullable: false),
                        ImagePath = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.goodstypes");
            DropTable("dbo.goods");
        }
    }
}
