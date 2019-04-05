namespace FoundryMissionsCom.Migrations.FoundryMissionsMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMissionExportText : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Missions", "MissionExportText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Missions", "MissionExportText", c => c.String());
        }
    }
}
