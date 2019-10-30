namespace RpshopingMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddeliveryaddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryAddresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DA_Name = c.String(),
                        DA_Phone = c.String(),
                        DA_Province = c.String(),
                        DA_City = c.String(),
                        DA_Town = c.String(),
                        DA_DetailedAddress = c.String(),
                        DA_ZipCode = c.String(),
                        DA_IsDefault = c.String(),
                        U_ID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DeliveryAddresses");
        }
    }
}
