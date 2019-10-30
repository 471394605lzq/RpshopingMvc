namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addintegraldetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IntegralDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        IntegralNumber = c.Int(nullable: false),
                        AddTime = c.String(),
                        AddType = c.String(),
                        AddOrReduce = c.Int(nullable: false),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IntegralDetails");
        }
    }
}
