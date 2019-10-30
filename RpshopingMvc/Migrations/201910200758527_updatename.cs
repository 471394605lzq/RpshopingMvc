namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.goodstypes", "Name", c => c.String(maxLength: 50));
            DropColumn("dbo.goodstypes", "SortName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.goodstypes", "SortName", c => c.String(maxLength: 50));
            DropColumn("dbo.goodstypes", "Name");
        }
    }
}
