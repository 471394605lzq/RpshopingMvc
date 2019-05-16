namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_userinfo", "sex", c => c.String(maxLength: 50));
            AddColumn("dbo.tb_userinfo", "birthday", c => c.String(maxLength: 50));
            AddColumn("dbo.tb_userinfo", "hometown", c => c.String(maxLength: 50));
            AddColumn("dbo.tb_userinfo", "residence", c => c.String(maxLength: 50));
            AddColumn("dbo.tb_userinfo", "signature", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_userinfo", "signature");
            DropColumn("dbo.tb_userinfo", "residence");
            DropColumn("dbo.tb_userinfo", "hometown");
            DropColumn("dbo.tb_userinfo", "birthday");
            DropColumn("dbo.tb_userinfo", "sex");
        }
    }
}
