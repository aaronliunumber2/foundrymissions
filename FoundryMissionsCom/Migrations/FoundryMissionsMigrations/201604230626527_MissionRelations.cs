namespace FoundryMissionsCom.Migrations.FoundryMissionsMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissionRelations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MissionTagTypeMissions",
                c => new
                    {
                        MissionTagType_Id = c.Int(nullable: false),
                        Mission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MissionTagType_Id, t.Mission_Id })
                .ForeignKey("dbo.MissionTagTypes", t => t.MissionTagType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Missions", t => t.Mission_Id, cascadeDelete: true)
                .Index(t => t.MissionTagType_Id)
                .Index(t => t.Mission_Id);
            
            CreateTable(
                "dbo.YoutubeVideoMissions",
                c => new
                    {
                        YoutubeVideo_YoutubeVideoId = c.String(nullable: false, maxLength: 128),
                        Mission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.YoutubeVideo_YoutubeVideoId, t.Mission_Id })
                .ForeignKey("dbo.YoutubeVideos", t => t.YoutubeVideo_YoutubeVideoId, cascadeDelete: true)
                .ForeignKey("dbo.Missions", t => t.Mission_Id, cascadeDelete: true)
                .Index(t => t.YoutubeVideo_YoutubeVideoId)
                .Index(t => t.Mission_Id);
            
            AlterColumn("dbo.Missions", "CrypticId", c => c.String(nullable: false, maxLength: 9));
            AlterColumn("dbo.Missions", "Name", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.MissionTagTypes", "TagName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Missions", "CrypticId", unique: true);
            CreateIndex("dbo.Missions", "Name", unique: true);
            CreateIndex("dbo.MissionTagTypes", "TagName", unique: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YoutubeVideoMissions", "Mission_Id", "dbo.Missions");
            DropForeignKey("dbo.YoutubeVideoMissions", "YoutubeVideo_YoutubeVideoId", "dbo.YoutubeVideos");
            DropForeignKey("dbo.MissionTagTypeMissions", "Mission_Id", "dbo.Missions");
            DropForeignKey("dbo.MissionTagTypeMissions", "MissionTagType_Id", "dbo.MissionTagTypes");
            DropIndex("dbo.YoutubeVideoMissions", new[] { "Mission_Id" });
            DropIndex("dbo.YoutubeVideoMissions", new[] { "YoutubeVideo_YoutubeVideoId" });
            DropIndex("dbo.MissionTagTypeMissions", new[] { "Mission_Id" });
            DropIndex("dbo.MissionTagTypeMissions", new[] { "MissionTagType_Id" });
            DropIndex("dbo.MissionTagTypes", new[] { "TagName" });
            DropIndex("dbo.Missions", new[] { "Name" });
            DropIndex("dbo.Missions", new[] { "CrypticId" });
            AlterColumn("dbo.MissionTagTypes", "TagName", c => c.String(nullable: false));
            AlterColumn("dbo.Missions", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Missions", "CrypticId", c => c.String(nullable: false));
            DropTable("dbo.YoutubeVideoMissions");
            DropTable("dbo.MissionTagTypeMissions");
        }
    }
}
