namespace FoundryMissionsCom.Migrations.FoundryMissionsMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Missions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Missions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorUserId = c.String(),
                        CrypticId = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Length = c.Int(nullable: false),
                        Faction = c.Int(nullable: false),
                        MinimumLevel = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateLastUpdated = c.DateTime(nullable: false),
                        Spotlit = c.Boolean(nullable: false),
                        Published = c.Boolean(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.MissionTagTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.YoutubeVideos",
                c => new
                    {
                        YoutubeVideoId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.YoutubeVideoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Missions", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Missions", new[] { "Author_Id" });
            DropTable("dbo.YoutubeVideos");
            DropTable("dbo.MissionTagTypes");
            DropTable("dbo.Missions");
        }
    }
}
