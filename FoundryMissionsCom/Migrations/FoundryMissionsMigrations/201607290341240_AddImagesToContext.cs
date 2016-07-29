namespace FoundryMissionsCom.Migrations.FoundryMissionsMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagesToContext : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MissionImageMissions", newName: "MissionMissionImages");
            DropPrimaryKey("dbo.MissionMissionImages");
            AddPrimaryKey("dbo.MissionMissionImages", new[] { "Mission_Id", "MissionImage_MissionId", "MissionImage_Filename" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MissionMissionImages");
            AddPrimaryKey("dbo.MissionMissionImages", new[] { "MissionImage_MissionId", "MissionImage_Filename", "Mission_Id" });
            RenameTable(name: "dbo.MissionMissionImages", newName: "MissionImageMissions");
        }
    }
}
