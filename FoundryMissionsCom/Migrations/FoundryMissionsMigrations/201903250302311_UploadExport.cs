namespace FoundryMissionsCom.Migrations.FoundryMissionsMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UploadExport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "MissionExportText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Missions", "MissionExportText");
        }
    }
}
