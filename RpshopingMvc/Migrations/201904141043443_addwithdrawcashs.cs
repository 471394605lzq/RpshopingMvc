namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addwithdrawcashs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Withdrawcashes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        out_biz_no = c.String(maxLength: 50),
                        order_id = c.String(maxLength: 50),
                        pay_date = c.String(maxLength: 50),
                        Userid = c.Int(nullable: false),
                        AliAccount = c.String(maxLength: 50),
                        UserName = c.String(maxLength: 50),
                        txamount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        txmonth = c.String(maxLength: 50),
                        signstr = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Withdrawcashes");
        }
    }
}
