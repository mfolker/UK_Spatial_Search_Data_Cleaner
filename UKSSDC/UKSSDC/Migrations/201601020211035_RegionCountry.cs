using System.Data.Entity.Migrations;

namespace UKSSDC.Migrations
{
    public partial class RegionCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Regions", "Country", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Regions", "Country");
        }
    }
}
