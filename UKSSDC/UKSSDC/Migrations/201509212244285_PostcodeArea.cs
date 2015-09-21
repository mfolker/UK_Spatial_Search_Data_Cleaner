namespace UKSSDC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostcodeArea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Postcodes", "Area", c => c.String());
            AddColumn("dbo.Postcodes", "Easting", c => c.Int(nullable: false));
            AddColumn("dbo.Postcodes", "Northing", c => c.Int(nullable: false));
            DropColumn("dbo.Postcodes", "Eastings");
            DropColumn("dbo.Postcodes", "Northings");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Postcodes", "Northings", c => c.Int(nullable: false));
            AddColumn("dbo.Postcodes", "Eastings", c => c.Int(nullable: false));
            DropColumn("dbo.Postcodes", "Northing");
            DropColumn("dbo.Postcodes", "Easting");
            DropColumn("dbo.Postcodes", "Area");
        }
    }
}
