namespace FoundryMissionsCom.Migrations.FoundryMissionsMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderImageVideos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.YoutubeVideoMissions", "YoutubeVideo_YoutubeVideoId", "dbo.YoutubeVideos");
            DropIndex("dbo.YoutubeVideoMissions", new[] { "YoutubeVideo_YoutubeVideoId" });
            DropPrimaryKey("dbo.YoutubeVideos");
            DropPrimaryKey("dbo.YoutubeVideoMissions");
            AddColumn("dbo.MissionImages", "Order", c => c.Int(nullable: false));
            AddColumn("dbo.YoutubeVideos", "MissionId", c => c.Int(nullable: false));
            AddColumn("dbo.YoutubeVideos", "Order", c => c.Int(nullable: false));
            AddColumn("dbo.YoutubeVideoMissions", "YoutubeVideo_MissionId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.YoutubeVideos", new[] { "MissionId", "YoutubeVideoId" });
            AddPrimaryKey("dbo.YoutubeVideoMissions", new[] { "YoutubeVideo_MissionId", "YoutubeVideo_YoutubeVideoId", "Mission_Id" });
            CreateIndex("dbo.YoutubeVideoMissions", new[] { "YoutubeVideo_MissionId", "YoutubeVideo_YoutubeVideoId" });
            AddForeignKey("dbo.YoutubeVideoMissions", new[] { "YoutubeVideo_MissionId", "YoutubeVideo_YoutubeVideoId" }, "dbo.YoutubeVideos", new[] { "MissionId", "YoutubeVideoId" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YoutubeVideoMissions", new[] { "YoutubeVideo_MissionId", "YoutubeVideo_YoutubeVideoId" }, "dbo.YoutubeVideos");
            DropIndex("dbo.YoutubeVideoMissions", new[] { "YoutubeVideo_MissionId", "YoutubeVideo_YoutubeVideoId" });
            DropPrimaryKey("dbo.YoutubeVideoMissions");
            DropPrimaryKey("dbo.YoutubeVideos");
            DropColumn("dbo.YoutubeVideoMissions", "YoutubeVideo_MissionId");
            DropColumn("dbo.YoutubeVideos", "Order");
            DropColumn("dbo.YoutubeVideos", "MissionId");
            DropColumn("dbo.MissionImages", "Order");
            AddPrimaryKey("dbo.YoutubeVideoMissions", new[] { "YoutubeVideo_YoutubeVideoId", "Mission_Id" });
            AddPrimaryKey("dbo.YoutubeVideos", "YoutubeVideoId");
            CreateIndex("dbo.YoutubeVideoMissions", "YoutubeVideo_YoutubeVideoId");
            AddForeignKey("dbo.YoutubeVideoMissions", "YoutubeVideo_YoutubeVideoId", "dbo.YoutubeVideos", "YoutubeVideoId", cascadeDelete: true);
        }
    }
}
