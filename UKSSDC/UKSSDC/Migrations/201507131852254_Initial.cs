namespace UKSSDC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportProgresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordType = c.Int(nullable: false),
                        FileName = c.String(nullable: false),
                        RecordNumber = c.Int(nullable: false),
                        Complete = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.Geography(),
                        OsmId = c.Int(nullable: false),
                        Country = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostCodePerimeters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutwardCode = c.String(),
                        Perimeter = c.Geography(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Postcode = c.String(),
                        PositionalQualityIndicator = c.String(),
                        Eastings = c.Int(nullable: false),
                        Northings = c.Int(nullable: false),
                        Location = c.Geography(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Perimeter = c.Geography(),
                        Type = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.Geography(),
                        OsmId = c.Int(nullable: false),
                        ReferenceNumber = c.String(),
                        Type = c.String(),
                        MaxSpeed = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Roads");
            DropTable("dbo.Regions");
            DropTable("dbo.PostCodes");
            DropTable("dbo.PostCodePerimeters");
            DropTable("dbo.Places");
            DropTable("dbo.ImportProgresses");
        }
    }
}
