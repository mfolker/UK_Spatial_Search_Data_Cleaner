namespace UKSSDC.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SpatialSeachObjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpatialSearchObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SpatialObject = c.Geography(),
                        Type = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpatialSearchObjects");
        }
    }
}
