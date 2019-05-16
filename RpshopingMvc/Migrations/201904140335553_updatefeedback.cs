namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefeedback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "BackTime", c => c.String(maxLength: 50));
            AlterColumn("dbo.Feedbacks", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "UserID", c => c.Int(nullable: false));
            DropColumn("dbo.Feedbacks", "BackTime");
        }
    }
}
