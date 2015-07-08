namespace UKSSDC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurnOnProgressTracking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportProgresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordType = c.Int(nullable: false),
                        FileName = c.String(),
                        RecordNumber = c.String(),
                        Complete = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImportProgresses");
        }
    }
}
