using System.Data.Entity.Migrations;

namespace UKSSDC.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.Geography(),
                        OsmId = c.Long(nullable: false),
                        Country = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostcodePerimeters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutwardCode = c.String(),
                        Perimeter = c.Geography(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Postcodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Area = c.String(),
                        FullPostcode = c.String(),
                        PositionalQualityIndicator = c.String(),
                        Easting = c.Int(nullable: false),
                        Northing = c.Int(nullable: false),
                        Location = c.Geography(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
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
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.Geography(),
                        OsmId = c.Long(nullable: false),
                        ReferenceNumber = c.String(),
                        Type = c.String(),
                        MaxSpeed = c.Int(nullable: false),
                        Country = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Roads");
            DropTable("dbo.Regions");
            DropTable("dbo.Postcodes");
            DropTable("dbo.PostcodePerimeters");
            DropTable("dbo.Places");
        }
    }
}
