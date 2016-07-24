using System.Data.Entity.Migrations;

namespace UKSSDC.Migrations
{
    public partial class AddOutwardCodeToPostcodes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Postcodes", "OutwardCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Postcodes", "OutwardCode");
        }
    }
}
