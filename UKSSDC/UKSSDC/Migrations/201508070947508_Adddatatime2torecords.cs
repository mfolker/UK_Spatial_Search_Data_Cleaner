namespace UKSSDC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adddatatime2torecords : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Places", "Created", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Places", "Updated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PostcodePerimeters", "Created", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PostcodePerimeters", "Updated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Postcodes", "Created", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Postcodes", "Updated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Regions", "Created", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Regions", "Updated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Roads", "Created", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Roads", "Updated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roads", "Updated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Roads", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Regions", "Updated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Regions", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Postcodes", "Updated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Postcodes", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PostcodePerimeters", "Updated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PostcodePerimeters", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Places", "Updated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Places", "Created", c => c.DateTime(nullable: false));
        }
    }
}
