namespace DBModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VehicleOwners",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VehicleID = c.Int(nullable: false),
                        OwnerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Owners", t => t.OwnerID, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleID, cascadeDelete: true)
                .Index(t => t.VehicleID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(),
                        VIN = c.String(),
                        VehicleTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeID, cascadeDelete: true)
                .Index(t => t.VehicleTypeID);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Mark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "VehicleTypeID", "dbo.VehicleTypes");
            DropForeignKey("dbo.VehicleOwners", "VehicleID", "dbo.Vehicles");
            DropForeignKey("dbo.VehicleOwners", "OwnerID", "dbo.Owners");
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeID" });
            DropIndex("dbo.VehicleOwners", new[] { "OwnerID" });
            DropIndex("dbo.VehicleOwners", new[] { "VehicleID" });
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Vehicles");
            DropTable("dbo.VehicleOwners");
            DropTable("dbo.Owners");
        }
    }
}
