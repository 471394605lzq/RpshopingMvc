namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecollegeinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CollegeInfoes", "Info", c => c.String());
            DropColumn("dbo.CollegeInfoes", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CollegeInfoes", "Content", c => c.String());
            DropColumn("dbo.CollegeInfoes", "Info");
        }
    }
}
