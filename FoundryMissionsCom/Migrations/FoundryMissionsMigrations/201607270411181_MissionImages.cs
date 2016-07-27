namespace FoundryMissionsCom.Migrations.FoundryMissionsMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissionImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MissionImages",
                c => new
                    {
                        MissionId = c.Int(nullable: false),
                        Filename = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MissionId, t.Filename });
            
            CreateTable(
                "dbo.MissionImageMissions",
                c => new
                    {
                        MissionImage_MissionId = c.Int(nullable: false),
                        MissionImage_Filename = c.String(nullable: false, maxLength: 128),
                        Mission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MissionImage_MissionId, t.MissionImage_Filename, t.Mission_Id })
                .ForeignKey("dbo.MissionImages", t => new { t.MissionImage_MissionId, t.MissionImage_Filename }, cascadeDelete: true)
                .ForeignKey("dbo.Missions", t => t.Mission_Id, cascadeDelete: true)
                .Index(t => new { t.MissionImage_MissionId, t.MissionImage_Filename })
                .Index(t => t.Mission_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MissionImageMissions", "Mission_Id", "dbo.Missions");
            DropForeignKey("dbo.MissionImageMissions", new[] { "MissionImage_MissionId", "MissionImage_Filename" }, "dbo.MissionImages");
            DropIndex("dbo.MissionImageMissions", new[] { "Mission_Id" });
            DropIndex("dbo.MissionImageMissions", new[] { "MissionImage_MissionId", "MissionImage_Filename" });
            DropTable("dbo.MissionImageMissions");
            DropTable("dbo.MissionImages");
        }
    }
}
