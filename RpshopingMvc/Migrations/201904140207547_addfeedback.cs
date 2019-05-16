namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfeedback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Titlestr = c.String(maxLength: 50),
                        Contentstr = c.String(),
                        Contactway = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.CollegeInfoes", "Code", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CollegeInfoes", "Code");
            DropTable("dbo.Feedbacks");
        }
    }
}
