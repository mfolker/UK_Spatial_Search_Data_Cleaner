using System.Data.Entity.Migrations;

namespace UKSSDC.Migrations
{
    public partial class AddImportProgress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportProgresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordType = c.Int(nullable: false),
                        FileName = c.String(nullable: false),
                        ProcessedRecords = c.Int(nullable: false),
                        TotalRecords = c.Int(nullable: false),
                        Complete = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImportProgresses");
        }
    }
}
