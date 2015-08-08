namespace UKSSDC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlacesOSMIDLong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Places", "OsmId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Places", "OsmId", c => c.Int(nullable: false));
        }
    }
}
