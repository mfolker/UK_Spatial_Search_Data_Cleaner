namespace UKSSDC.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCountryFromRegion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Regions", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Regions", "Country", c => c.Int(nullable: false));
        }
    }
}
