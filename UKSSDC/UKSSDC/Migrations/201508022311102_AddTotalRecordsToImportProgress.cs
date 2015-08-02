using System.Data.Entity.Migrations;

namespace UKSSDC.Migrations
{
    public partial class AddTotalRecordsToImportProgress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImportProgresses", "ProcessedRecords", c => c.Int(nullable: false));
            AddColumn("dbo.ImportProgresses", "TotalRecords", c => c.Int(nullable: false));
            DropColumn("dbo.ImportProgresses", "RecordNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImportProgresses", "RecordNumber", c => c.Int(nullable: false));
            DropColumn("dbo.ImportProgresses", "TotalRecords");
            DropColumn("dbo.ImportProgresses", "ProcessedRecords");
        }
    }
}
