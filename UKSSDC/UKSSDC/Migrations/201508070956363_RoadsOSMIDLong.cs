namespace UKSSDC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoadsOSMIDLong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Roads", "OsmId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roads", "OsmId", c => c.Int(nullable: false));
        }
    }
}
