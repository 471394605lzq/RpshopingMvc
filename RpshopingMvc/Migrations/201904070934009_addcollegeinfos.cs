namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcollegeinfos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CollegeInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Content = c.String(),
                        Title = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CollegeInfoes");
        }
    }
}
